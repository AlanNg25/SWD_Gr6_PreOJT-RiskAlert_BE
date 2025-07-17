using Applications.DTO.Create;
using Applications.DTO.Response;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Repositories.Interfaces;
using Repositories.Models;
using Repositories.Repositories;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Services.Implement
{
    public class AiService : IAiService
    {
        private readonly Kernel _kernel;
        private readonly IChatCompletionService _chatService;
        private readonly IServiceProviders _serviceProviders;
        private readonly ConcurrentDictionary<string, ChatHistory> _chatHistories;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public AiService(
            Kernel kernel,
            IChatCompletionService chatService,
            IServiceProviders serviceProviders,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _kernel = kernel ?? throw new ArgumentNullException(nameof(kernel));
            _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
            _serviceProviders = serviceProviders ?? throw new ArgumentNullException(nameof(serviceProviders));
            _chatHistories = new ConcurrentDictionary<string, ChatHistory>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task AnalyzeAllStudentsAsync()
        {
            var students = await _serviceProviders.UserService.GetByRoleAsync("Student");

            foreach (var student in students)
            {
                var enrollments = await _serviceProviders.EnrollmentService.GetAllAsync();
                enrollments = enrollments.Where(e => e.StudentID == student.UserID && !e.IsDeleted);
                foreach (var enrollment in enrollments)
                {
                    var course = await _serviceProviders.CourseService.GetByIdAsync(enrollment.CourseID);
                    if (await IsStudentEligibleAsync(student, course))
                    {
                        await AnalyzeStudentAsync(student.UserID, enrollment.EnrollmentID);
                    }
                }
            }

            await _unitOfWork.SaveChangesWithTransactionAsync();

            // Schedule periodic analysis (BR02)
            // In production, use a background job (e.g., Hangfire) for weekly runs
        }

        public async Task<RiskAnalysisDto> AnalyzeStudentAsync(Guid studentId, Guid courseId)
        {
            var student = await _serviceProviders.UserService.GetByIdAsync(studentId);
            var course = await _serviceProviders.CourseService.GetByIdAsync(courseId);
            if (!await IsStudentEligibleAsync(student, course))
                return null;
            var enrollments = await _serviceProviders.EnrollmentService.GetAllAsync();
            var enrollment = enrollments.FirstOrDefault(e => e.StudentID == studentId && e.CourseID == courseId && !e.IsDeleted);
            if (enrollment == null)
                return null;


            var attendance = await _serviceProviders.AttendanceService.GetByUserIdAsync(studentId);
            attendance = attendance.Where(a => a.EnrollmentID == enrollment.EnrollmentID && !a.IsDeleted).ToList();
            var grades = await _serviceProviders.GradeService.GetByUserIdAsync(studentId);
            grades = grades.Where(g => g.CourseID == courseId && !g.IsDeleted).ToList();
            var grade = grades.FirstOrDefault(g => g.CourseID == courseId && !g.IsDeleted);
            var notifications = await _serviceProviders.NotificationService.GetByUserIdAsync(studentId);
            var interactionFrequency = notifications.Count(n => n.SentTime >= DateTime.Now.AddDays(-7));
            var absenceRate = attendance.Any() ? attendance.Sum(a => a.SessionNumber - a.AttendNumber) / (double)attendance.Sum(a => a.SessionNumber) : 0;
            var averageGrade = grade.ScoreAverage;
            var consecutiveAbsences = attendance.Any() ? attendance.Max(a => CalculateConsecutiveAbsences(a)) : 0;
            var unexcusedAbsences = attendance.Sum(a => a.Notes?.ToLower().Contains("unexcused") == true ? 1 : 0);
            var isOjtPrerequisite = await IsOjtPrerequisiteAsync(course);

            // Check prerequisites (BR16, FR16)
            var prerequisiteStatus = await CheckPrerequisitePerformanceAsync(studentId, course);

            int riskLevel = 0;
            if (absenceRate >= 0.2) riskLevel++;
            if (averageGrade < 5) riskLevel++;
            if (interactionFrequency < 3) riskLevel++;
            if (consecutiveAbsences >= 3) riskLevel++;
            if (averageGrade < 4) riskLevel = Math.Max(riskLevel, 1);
            if (isOjtPrerequisite) riskLevel++;
            if (unexcusedAbsences > 0) riskLevel++;
            if (!prerequisiteStatus.Passed) riskLevel++;

            var prompt = $@"Analyze the following student data for risk of failing:
        Student ID: {studentId}
        Course: {course.CourseCode} (OJT Prerequisite: {isOjtPrerequisite})
        Absence Rate: {absenceRate:P0}
        Average Grade: {averageGrade}
        LMS Interactions (last 7 days): {interactionFrequency}
        Consecutive Absences: {consecutiveAbsences}
        Unexcused Absences: {unexcusedAbsences}
        Prerequisite Status: {(prerequisiteStatus.Passed ? "Passed" : "Not Passed")}
        Provide a risk level (0=Safe, 1=Needs Attention, 2=High Risk) and a personalized recommendation (Student/Instructor/School action).";

            var chatHistory = _chatHistories.GetOrAdd($"{studentId}-{courseId}", new ChatHistory("You are an educational AI analyst."));
            chatHistory.AddUserMessage(prompt);
            var response = await _chatService.GetChatMessageContentAsync(chatHistory);
            chatHistory.AddAssistantMessage(response.ToString());

            // Parse AI response
            var aiResult = ParseAiResponse(response.ToString(), riskLevel);
            var riskAnalysisDto = new RiskAnalysisCreateDto
            {
                EnrollmentID = enrollment.EnrollmentID,
                RiskLevel = aiResult.RiskLevel.ToString(),
                TrackingDate = DateTime.Now,
                Notes = $"AI Analysis: {response}",
                IsResolved = aiResult.RiskLevel == 0,
            };
            await _serviceProviders.RiskAnalysisService.AddAsync(riskAnalysisDto);

            var riskAnalyses = await _serviceProviders.RiskAnalysisService.GetAllAsync();
            var createdRiskAnalysis = riskAnalyses
                .Where(r => r.EnrollmentID == enrollment.EnrollmentID && r.TrackingDate == riskAnalysisDto.TrackingDate)
                .OrderByDescending(r => r.TrackingDate)
                .FirstOrDefault();
            if (createdRiskAnalysis == null)
                throw new InvalidOperationException("Failed to retrieve created RiskAnalysis.");

            var prediction = new PredictionCreateDto
            {
                StudentID = studentId,
                Probability = CalculateRiskProbability(riskLevel),
                PredictionDate = DateTime.Now,
                Details = $"Risk Level: {aiResult.RiskLevel}, Reason: {response}"
            };
            await _serviceProviders.PredictionService.AddAsync(prediction);

            if (aiResult.RiskLevel >= 1 && await CanSendWarningAsync(enrollment.EnrollmentID))
            {
                var notification = new NotificationCreateDto
                {
                    ReceiverID = studentId,
                    Content = $"You are at risk of failing {course.CourseCode} (Risk Level: {aiResult.RiskLevel}). {aiResult.Recommendation}",
                    Status = 0,
                    SentTime = DateTime.Now,
                    Attachment = ""
                };
                await _serviceProviders.NotificationService.AddAsync(notification);

                var suggestion = new SuggestionCreateDto
                {
                    RiskID = createdRiskAnalysis.RiskID,
                    AdvisorID = course.TeacherID,
                    ActionType = aiResult.RecommendationType,
                    Notes = aiResult.Recommendation
                };
                await _serviceProviders.SuggestionService.AddAsync(suggestion);
            }

            if (aiResult.RiskLevel == 0)
            {
                createdRiskAnalysis.IsResolved = true;
                await _serviceProviders.RiskAnalysisService.UpdateAsync(createdRiskAnalysis.RiskID, _mapper.Map<RiskAnalysisCreateDto>(createdRiskAnalysis));
            }

            await _unitOfWork.SaveChangesWithTransactionAsync();
            return createdRiskAnalysis;
        }

        public async Task<IEnumerable<RiskAnalysisDto>> GetRiskAnalysisForCourseAsync(Guid courseId)
        {
            var enrollments = await _serviceProviders.EnrollmentService.GetAllAsync();
            var riskAnalyses = await _serviceProviders.RiskAnalysisService.GetAllAsync();
            return riskAnalyses.Where(r => enrollments.Any(e => e.EnrollmentID == r.EnrollmentID && e.CourseID == courseId && !e.IsDeleted));
        }

        public async Task<IEnumerable<NotificationWithCourseSemesterDto>> GetWarningsForStudentAsync(Guid studentId)
        {
            return await _serviceProviders.NotificationService.GetByUserIdAsync(studentId);
        }

        public async Task RespondToWarningAsync(Guid notificationId, string studentResponse)
        {
            var notification = await _serviceProviders.NotificationService.GetByIdAsync(notificationId);
            if (notification == null || notification.SentTime < DateTime.Now.AddDays(-5))
                throw new InvalidOperationException("Notification not found or response window expired.");

            var notificationUpdateDto = _mapper.Map<NotificationCreateDto>(notification);
            notificationUpdateDto.Status = string.IsNullOrEmpty(studentResponse) ? 2 : 1; // 2=Unresolved, 1=Responded
            await _serviceProviders.NotificationService.UpdateAsync(notificationId, notificationUpdateDto);

            var suggestions = await _serviceProviders.SuggestionService.GetAllAsync();
            var riskAnalyses = await _serviceProviders.RiskAnalysisService.GetAllAsync();
            var enrollments = await _serviceProviders.EnrollmentService.GetAllAsync();
            var suggestion = suggestions
                .Where(s => riskAnalyses.Any(r => r.RiskID == s.RiskID && r.TrackingDate >= notification.SentTime)
                            && enrollments.Any(e => e.EnrollmentID == riskAnalyses.First(r => r.RiskID == s.RiskID).EnrollmentID && e.StudentID == notification.ReceiverID))
                .OrderByDescending(s => s.SentDate)
                .FirstOrDefault();

            if (suggestion != null)
            {
                var suggestionUpdateDto = _mapper.Map<SuggestionCreateDto>(suggestion);
                suggestionUpdateDto.ActionDate = DateTime.Now;
                suggestionUpdateDto.Notes = $"{suggestion.Notes}\nStudent Response: {studentResponse}";
                await _serviceProviders.SuggestionService.UpdateAsync(suggestion.SuggestionID, suggestionUpdateDto);
            }

            await _unitOfWork.SaveChangesWithTransactionAsync();
        }

        private async Task<bool> CanSendWarningAsync(Guid enrollmentId)
        {
            var notifications = await _serviceProviders.NotificationService.GetAllAsync();
            var enrollments = await _serviceProviders.EnrollmentService.GetAllAsync();
            var lastWarning = notifications
                .Where(n => enrollments.Any(e => e.EnrollmentID == enrollmentId && e.StudentID == n.ReceiverID))
                .OrderByDescending(n => n.SentTime)
                .FirstOrDefault();
            return lastWarning == null || lastWarning.SentTime < DateTime.Now.AddDays(-7);
        }

        private async Task<bool> IsStudentEligibleAsync(UserDto student, CourseDto course)
        {
            if (student == null || course == null || student.Role != "Student")
                return false;

            var enrollments = await _serviceProviders.EnrollmentService.GetAllAsync();
            var studentEnrollments = enrollments.Where(e => e.StudentID == student.UserID && !e.IsDeleted);
            var semesterCount = studentEnrollments
                .GroupBy(e => e.CourseID)
                .Count();
            if (semesterCount > 5)
                return false;

            var semester = await _serviceProviders.SemesterService.GetSemesterByIdAsync(course.SemesterID);
            if (semester == null)
                return false;
            return semester.StartDate <= DateTime.Now && semester.EndDate >= DateTime.Now;
        }

        private async Task<bool> IsOjtPrerequisiteAsync(CourseDto course)
        {
            var syllabuses = await _serviceProviders.SyllabusService.GetAllAsync();
            return syllabuses.Any(s => s.SubjectID == course.SubjectID && s.Type == "OJT Prerequisite");
        }

        private async Task<(bool Passed, string Details)> CheckPrerequisitePerformanceAsync(Guid studentId, CourseDto course)
        {
            var syllabuses = await _serviceProviders.SyllabusService.GetAllAsync();
            var courseSyllabus = syllabuses.FirstOrDefault(s => s.SubjectID == course.SubjectID && !s.IsDeleted);
            if (courseSyllabus?.PrerequisitesSubjectID == null)
                return (true, "No prerequisites required.");

            var prerequisiteSubjectId = courseSyllabus.PrerequisitesSubjectID.Value;
            var prerequisiteCourses = await _serviceProviders.CourseService.GetAllAsync();
            prerequisiteCourses = prerequisiteCourses.Where(c => c.SubjectID == prerequisiteSubjectId && !c.IsDeleted);

            var enrollments = await _serviceProviders.EnrollmentService.GetAllAsync();
            var prerequisiteEnrollments = enrollments
                .Where(e => e.StudentID == studentId && prerequisiteCourses.Any(c => c.CourseID == e.CourseID) && !e.IsDeleted);

            var grades = await _serviceProviders.GradeService.GetByUserIdAsync(studentId);
            var prerequisiteGrades = grades
                .Where(g => prerequisiteEnrollments.Any(e => e.CourseID == g.CourseID))
                .ToList();

            var passed = prerequisiteGrades.Any() && prerequisiteGrades.All(g => g.ScoreAverage >= 5.0m); // 0-10 scale
            var details = passed ? "Prerequisites passed." : "Failed one or more prerequisite courses.";
            return (passed, details);
        }

        private int CalculateConsecutiveAbsences(AttendanceWithCourseSemesterDto attendance)
        {
            return attendance.SessionNumber - attendance.AttendNumber >= 3 ? 3 : 0;
        }

        private decimal CalculateRiskProbability(int riskLevel)
        {
            return riskLevel switch
            {
                0 => 0.1m,
                1 => 0.5m,
                2 => 0.9m,
                _ => 0.5m
            };
        }

        private (int RiskLevel, string Recommendation, string RecommendationType) ParseAiResponse(string response, int calculatedRiskLevel)
        {
            // Simplified parsing; assumes AI response contains structured data
            var riskLevel = calculatedRiskLevel;
            var recommendation = riskLevel >= 2 ? "Schedule a meeting with your academic advisor." : "Increase LMS interactions and submit assignments on time.";
            var recommendationType = riskLevel >= 2 ? "School" : "Student";
            if (riskLevel == 0) recommendation = "Continue your current performance.";
            return (riskLevel, recommendation, recommendationType);
        }
    }
}