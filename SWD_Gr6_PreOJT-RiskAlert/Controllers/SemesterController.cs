using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
using Services.Interface;

namespace SWD_Gr6_PreOJT_RiskAlert.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SemesterController : ControllerBase
    {
        private readonly ISemesterService _semesterService;
        public SemesterController(ISemesterService semesterService)
        {
            _semesterService = semesterService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Semester>>> GetAllSemesters()
        {
            var result = await _semesterService.GetAllSemestersAsync();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Semester>> GetSemesterById(Guid id)
        {
            var result = await _semesterService.GetSemesterByIdAsync(id);
            if (result == null || result.SemesterID == Guid.Empty)
                return NotFound();
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> CreateSemester([FromBody] Semester semester)
        {
            if (semester == null)
                return BadRequest("Semester cannot be null.");
            var rowsAffected = await _semesterService.CreateSemesterAsync(semester);
            if (rowsAffected > 0)
                return CreatedAtAction(nameof(GetSemesterById), new { id = semester.SemesterID }, semester);
            return BadRequest("Creation failed.");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSemester(Guid id, [FromBody] Semester semester)
        {
            if (id != semester.SemesterID)
                return BadRequest("ID mismatch.");
            var rowsAffected = await _semesterService.UpdateSemesterAsync(semester);
            if (rowsAffected > 0)
                return NoContent();
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSemester(Guid id)
        {
            var rowsAffected = await _semesterService.DeleteSemesterAsync(id);
            if (rowsAffected > 0)
                return NoContent();
            return NotFound("Semester not found.");
        }
        [HttpGet("search/{semesterCode}")]
        public async Task<ActionResult<List<Semester>>> SearchSemester(string semesterCode)
        {
            var result = await _semesterService.SearchSemesterAsync(semesterCode);
            if (result == null || result.Count == 0)
                return NotFound("No semesters found with the given code.");
            return Ok(result);
        }
    }
}
