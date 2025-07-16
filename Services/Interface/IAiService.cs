using Applications.DTO.Response;
using Repositories.Models;

namespace Services.Interface
{
    public interface IAiService
    {
        Task AnalyzeAllStudentsAsync();
        Task<RiskAnalysisDto> AnalyzeStudentAsync(Guid studentId, Guid courseId);
        Task<IEnumerable<RiskAnalysisDto>> GetRiskAnalysisForCourseAsync(Guid courseId);
        Task<IEnumerable<NotificationWithCourseSemesterDto>> GetWarningsForStudentAsync(Guid studentId);
        Task RespondToWarningAsync(Guid notificationId, string studentResponse);
    }
}