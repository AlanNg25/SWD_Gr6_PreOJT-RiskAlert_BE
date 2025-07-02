using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Repositories.Repositories;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public interface IServiceProviders
    {
        IAttendanceService AttendanceService { get; }
        ICourseService CourseService { get; }
        ICurriculumService CurriculumService { get; }
        IEnrollmentService EnrollmentService { get; }
        IGradeService GradeService { get; }
        IMajorService MajorService { get; }
        INotificationService NotificationService { get; }
        IPredictionService PredictionService { get; }
        IRiskAnalysisService RiskAnalysisService { get; }
        ISemesterService SemesterService { get; }
        ISubjectService SubjectService { get; }
        ISuggestionService SuggestionService { get; }
        ISyllabusService SyllabusService { get; }
        IUserService UserService { get; }
        IAuthService AuthService { get; }
    }

    public class ServiceProviders : IServiceProviders
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private IConfiguration _configuration;

        public ServiceProviders(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        private IAttendanceService _attendanceService;
        public IAttendanceService AttendanceService
        {
            get { return _attendanceService ??= new AttendanceService(_unitOfWork, _mapper); }
        }

        private ICourseService _courseService;
        public ICourseService CourseService
        {
            get { return _courseService ??= new CourseService(_unitOfWork, _mapper); }
        }

        private ICurriculumService _curriculumService;
        public ICurriculumService CurriculumService
        {
            get { return _curriculumService ??= new CurriculumService(_unitOfWork, _mapper); }
        }

        private IEnrollmentService _enrollmentService;
        public IEnrollmentService EnrollmentService
        {
            get { return _enrollmentService ??= new EnrollmentService(_unitOfWork, _mapper); }
        }

        private IGradeService _gradeService;
        public IGradeService GradeService
        {
            get { return _gradeService ??= new GradeService(_unitOfWork, _mapper); }
        }

        private IMajorService _majorService;
        public IMajorService MajorService
        {
            get { return _majorService ??= new MajorService(_unitOfWork, _mapper); }
        }

        private INotificationService _notificationService;
        public INotificationService NotificationService
        {
            get { return _notificationService ??= new NotificationService(_unitOfWork, _mapper); }
        }

        private IPredictionService _predictionService;
        public IPredictionService PredictionService
        {
            get { return _predictionService ??= new PredictionService(_unitOfWork, _mapper); }
        }

        private IRiskAnalysisService _riskAnalysisService;
        public IRiskAnalysisService RiskAnalysisService
        {
            get { return _riskAnalysisService ??= new RiskAnalysisService(_unitOfWork, _mapper); }
        }

        private ISemesterService _semesterService;
        public ISemesterService SemesterService
        {
            get { return _semesterService ??= new SemesterService(_unitOfWork, _mapper); }
        }

        private ISubjectService _subjectService;
        public ISubjectService SubjectService
        {
            get { return _subjectService ??= new SubjectService(_unitOfWork, _mapper); }
        }

        private ISuggestionService _suggestionService;
        public ISuggestionService SuggestionService
        {
            get { return _suggestionService ??= new SuggestionService(_unitOfWork, _mapper); }
        }

        private ISyllabusService _syllabusService;
        public ISyllabusService SyllabusService
        {
            get { return _syllabusService ??= new SyllabusService(_unitOfWork, _mapper); }
        }

        private IUserService _userService;
        public IUserService UserService
        {
            get { return _userService ??= new UserService(_unitOfWork, _mapper); }
        }

        private IAuthService _authService;
        public IAuthService AuthService
        {
            get { return _authService ??= new AuthService(_unitOfWork, _configuration); }
        }
    }
}
