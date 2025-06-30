using Applications.DTO.Create;
using Microsoft.AspNetCore.Mvc;
using Services.Implement;

namespace SWD_Gr6_PreOJT_RiskAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IServiceProviders _serviceProviders;

        public AttendanceController(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var attendances = await _serviceProviders.AttendanceService.GetAllAsync();
                return Ok(attendances);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var attendance = await _serviceProviders.AttendanceService.GetByIdAsync(id);
                return Ok(attendance);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttendanceCreateDto attendanceDto)
        {
            try
            {
                await _serviceProviders.AttendanceService.AddAsync(attendanceDto);
                return CreatedAtAction(nameof(GetById), new { id = Guid.NewGuid() }, attendanceDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AttendanceCreateDto attendanceDto)
        {
            try
            {
                await _serviceProviders.AttendanceService.UpdateAsync(id, attendanceDto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _serviceProviders.AttendanceService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                var results = await _serviceProviders.AttendanceService.GetByUserIdAsync(userId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
