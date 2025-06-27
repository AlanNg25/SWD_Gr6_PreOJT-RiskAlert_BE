using Applications.DTO.Create;
using Microsoft.AspNetCore.Mvc;
using Services.Implement;

namespace SWD_Gr6_PreOJT_RiskAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskAnalysisController : ControllerBase
    {
        private readonly IServiceProviders _serviceProviders;

        public RiskAnalysisController(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var riskAnalyses = await _serviceProviders.RiskAnalysisService.GetAllAsync();
                return Ok(riskAnalyses);
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
                var riskAnalysis = await _serviceProviders.RiskAnalysisService.GetByIdAsync(id);
                return Ok(riskAnalysis);
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
        public async Task<IActionResult> Create([FromBody] RiskAnalysisCreateDto riskAnalysisDto)
        {
            try
            {
                await _serviceProviders.RiskAnalysisService.AddAsync(riskAnalysisDto);
                return CreatedAtAction(nameof(GetById), new { id = Guid.NewGuid() }, riskAnalysisDto);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] RiskAnalysisCreateDto riskAnalysisDto)
        {
            try
            {
                await _serviceProviders.RiskAnalysisService.UpdateAsync(id, riskAnalysisDto);
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
                await _serviceProviders.RiskAnalysisService.DeleteAsync(id);
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
    }
}
