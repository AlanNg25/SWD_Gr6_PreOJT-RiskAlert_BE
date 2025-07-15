using Applications.DTO.Create;
using Applications.DTO.Response;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<SemesterDto>>> GetAllSemesters()
        {
            var result = await _serviceProviders.SemesterService.GetAllSemestersAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<SemesterDto>> GetSemesterById(Guid id)
        {
            var result = await _serviceProviders.SemesterService.GetSemesterByIdAsync(id);
            if (result == null || result.SemesterID == Guid.Empty)
                return NotFound();
            return Ok(result);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateSemester([FromBody] SemesterCreateDto semesterDto)
        {
            if (semesterDto == null)
                return BadRequest("Semester cannot be null.");

            var semesterId = await _serviceProviders.SemesterService.CreateSemesterAsync(semesterDto);
            if (semesterId != Guid.Empty)
            {
                var createdSemester = await _serviceProviders.SemesterService.GetSemesterByIdAsync(semesterId);
                return CreatedAtAction(nameof(GetSemesterById), new { id = createdSemester.SemesterID }, createdSemester);
            }

            return BadRequest("Creation failed.");
        }


        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateSemester(Guid id, [FromBody] SemesterCreateDto semesterDto)
        {
            if (semesterDto == null)
                return BadRequest("Semester cannot be null.");

            var rowsAffected = await _serviceProviders.SemesterService.UpdateSemesterAsync(id, semesterDto);
            if (rowsAffected > 0)
                return NoContent();

            return NotFound();
        }


        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteSemester(Guid id)
        {
            var rowsAffected = await _serviceProviders.SemesterService.DeleteSemesterAsync(id);
            if (rowsAffected > 0)
                return NoContent();
            return NotFound("Semester not found.");
        }

        [Authorize]
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
