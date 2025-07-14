using Applications.DTO.Create;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
using Services.Implement;
using Services.Interface;

namespace SWD_Gr6_PreOJT_RiskAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IServiceProviders _serviceProviders;
        private readonly IMapper _mapper;

        public GradeController(IServiceProviders serviceProviders, IMapper mapper)
        {
            _serviceProviders = serviceProviders;
            _mapper = mapper;
        }

        // Grade CRUD
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var grades = await _serviceProviders.GradeService.GetAllAsync();
                return Ok(grades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var grade = await _serviceProviders.GradeService.GetByIdAsync(id);
                return Ok(grade);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Grade not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GradeCreateDto gradeCreateDto)
        {
            try
            {
                var grade = _mapper.Map<Grade>(gradeCreateDto);
                grade.GradeID = Guid.NewGuid(); // nếu bạn muốn tự generate ID

                await _serviceProviders.GradeService.AddAsync(grade);

                return CreatedAtAction(nameof(GetById), new { id = grade.GradeID }, grade);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GradeCreateDto gradeUpdateDto)
        {
            try
            {
                await _serviceProviders.GradeService.UpdateAsync(id, gradeUpdateDto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Grade not found");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _serviceProviders.GradeService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GradeDetail CRUD
        [Authorize]
        [HttpGet("{gradeId}/details")]
        public async Task<IActionResult> GetGradeDetails(Guid gradeId)
        {
            try
            {
                var gradeDetails = await _serviceProviders.GradeService.GetGradeDetailsByGradeIdAsync(gradeId);
                return Ok(gradeDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetGradeDetailById(Guid id)
        {
            try
            {
                var gradeDetail = await _serviceProviders.GradeService.GetGradeDetailByIdAsync(id);
                return Ok(gradeDetail);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("GradeDetail not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPost("details")]
        public async Task<IActionResult> CreateGradeDetail([FromBody] GradeDetail gradeDetail)
        {
            try
            {
                await _serviceProviders.GradeService.AddGradeDetailAsync(gradeDetail);
                return CreatedAtAction(nameof(GetGradeDetailById), new { id = gradeDetail.GradeDetailID }, gradeDetail);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpPut("details/{id}")]
        public async Task<IActionResult> UpdateGradeDetail(Guid id, [FromBody] GradeDetail gradeDetail)
        {
            if (id != gradeDetail.GradeDetailID)
                return BadRequest("GradeDetail ID mismatch");

            try
            {
                await _serviceProviders.GradeService.UpdateGradeDetailAsync(gradeDetail);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpDelete("details/{id}")]
        public async Task<IActionResult> DeleteGradeDetail(Guid id)
        {
            try
            {
                await _serviceProviders.GradeService.DeleteGradeDetailAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            try
            {
                var result = await _serviceProviders.GradeService.GetByUserIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
