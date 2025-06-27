using Applications.DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
using Services.Implement;
using Services.Interface;

namespace SWD_Gr6_PreOJT_RiskAlert.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SemesterController : ControllerBase
    {
        private readonly IServiceProviders _serviceProviders;
        public SemesterController(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }
        [HttpGet]
        public async Task<ActionResult<List<SemesterDto>>> GetAllSemesters()
        {
            var result = await _serviceProviders.SemesterService.GetAllSemestersAsync();
            return Ok(result);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SemesterDto>> GetSemesterById(Guid id)
        {
            var result = await _serviceProviders.SemesterService.GetSemesterByIdAsync(id);
            if (result == null || result.SemesterID == Guid.Empty)
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> CreateSemester([FromBody] SemesterDto semester)
        {
            if (semester == null)
                return BadRequest("Semester cannot be null.");
            var rowsAffected = await _serviceProviders.SemesterService.CreateSemesterAsync(semester);
            if(rowsAffected > 0)
            {
                var createdSemester = await _serviceProviders.SemesterService.GetSemesterByIdAsync(semester.SemesterID);
                return CreatedAtAction(nameof(GetSemesterById), new { id = createdSemester.SemesterID }, createdSemester);
            }
            return BadRequest("Creation failed.");
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateSemester(Guid id, [FromBody] SemesterDto semester)
        {
            if (semester == null)
                return BadRequest("Semester cannot be null.");
            if (id != semester.SemesterID)
                return BadRequest("ID mismatch.");
            var rowsAffected = await _serviceProviders.SemesterService.UpdateSemesterAsync(semester);
            if (rowsAffected > 0)
                return NoContent();
            return NotFound();
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteSemester(Guid id)
        {
            var rowsAffected = await _serviceProviders.SemesterService.DeleteSemesterAsync(id);
            if (rowsAffected > 0)
                return NoContent();
            return NotFound("Semester not found.");
        }
        [HttpGet("semesterCode")]
        public async Task<ActionResult<List<SemesterDto>>> SearchSemester(string semesterCode)
        {
            if (string.IsNullOrEmpty(semesterCode))
                return BadRequest("Semester code cannot be null or empty.");
            var result = await _serviceProviders.SemesterService.SearchSemesterAsync(semesterCode);
            if (result == null || result.Count == 0)
                return NotFound("No semesters found with the provided code.");
            return Ok(result);
        }
    }
}
