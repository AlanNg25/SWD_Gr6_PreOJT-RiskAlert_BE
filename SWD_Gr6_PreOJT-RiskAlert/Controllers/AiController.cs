using Microsoft.AspNetCore.Mvc;
using Services.Implement;
using Services.Interface;

namespace SWD_Gr6_PreOJT_RiskAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly IAiService _aiService;

        public AiController(IAiService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("analyses")]
        public async Task<IActionResult> AnalyzeAllStudentsAsync()
        {
            try
            {
                await _aiService.AnalyzeAllStudentsAsync();
                return Ok(new { Message = "Risk analysis completed for all eligible students." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while analyzing students.", Details = ex.Message });
            }
        }

        [HttpPost("analyses/student/{studentId}/course/{courseId}")]
        public async Task<IActionResult> AnalyzeStudentAsync(Guid studentId, Guid courseId)
        {
            if (studentId == Guid.Empty || courseId == Guid.Empty)
                return BadRequest(new { Error = "Student ID and Course ID must be valid GUIDs." });

            try
            {
                var riskAnalysis = await _aiService.AnalyzeStudentAsync(studentId, courseId);
                if (riskAnalysis == null)
                    return NotFound(new { Error = "Student or course not found, or student is not eligible for analysis." });

                return Ok(riskAnalysis);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while analyzing the student.", Details = ex.Message });
            }
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetRiskAnalysisForCourseAsync(Guid courseId)
        {
            if (courseId == Guid.Empty)
                return BadRequest(new { Error = "Course ID must be a valid GUID." });

            try
            {
                var riskAnalyses = await _aiService.GetRiskAnalysisForCourseAsync(courseId);
                return Ok(riskAnalyses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while retrieving risk analyses.", Details = ex.Message });
            }
        }

        [HttpGet("student/{studentId}")]
        public async Task<IActionResult> GetWarningsForStudentAsync(Guid studentId)
        {
            if (studentId == Guid.Empty)
                return BadRequest(new { Error = "Student ID must be a valid GUID." });

            try
            {
                var warnings = await _aiService.GetWarningsForStudentAsync(studentId);
                return Ok(warnings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while retrieving warnings.", Details = ex.Message });
            }
        }

        [HttpPost("respond-warning/{notificationId}")]
        public async Task<IActionResult> RespondToWarningAsync(Guid notificationId, [FromBody] StudentResponseRequest request)
        {
            if (notificationId == Guid.Empty)
                return BadRequest(new { Error = "Notification ID must be a valid GUID." });
            if (request == null || string.IsNullOrWhiteSpace(request.StudentResponse))
                return BadRequest(new { Error = "Student response is required." });

            try
            {
                await _aiService.RespondToWarningAsync(notificationId, request.StudentResponse);
                return Ok(new { Message = "Response recorded successfully." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An error occurred while recording the response.", Details = ex.Message });
            }
        }
    }

    public class StudentResponseRequest
    {
        public string StudentResponse { get; set; }
    }
}
