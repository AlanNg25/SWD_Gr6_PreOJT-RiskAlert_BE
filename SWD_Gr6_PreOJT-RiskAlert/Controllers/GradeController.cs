using Applications.DTO.Create;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
using Services.Interface;

namespace SWD_Gr6_PreOJT_RiskAlert.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        private readonly IMapper _mapper;

        public GradeController(IGradeService gradeService, IMapper mapper)
        {
            _gradeService = gradeService;
            _mapper = mapper;
        }

        // Grade CRUD
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var grades = await _gradeService.GetAllAsync();
                return Ok(grades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var grade = await _gradeService.GetByIdAsync(id);
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GradeCreateDto gradeCreateDto)
        {
            try
            {
                var grade = _mapper.Map<Grade>(gradeCreateDto);
                grade.GradeID = Guid.NewGuid(); // nếu bạn muốn tự generate ID

                await _gradeService.AddAsync(grade);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Grade grade)
        {
            if (id != grade.GradeID)
                return BadRequest("Grade ID mismatch");

            try
            {
                await _gradeService.UpdateAsync(grade);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _gradeService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GradeDetail CRUD
        [HttpGet("{gradeId}/details")]
        public async Task<IActionResult> GetGradeDetails(Guid gradeId)
        {
            try
            {
                var gradeDetails = await _gradeService.GetGradeDetailsByGradeIdAsync(gradeId);
                return Ok(gradeDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetGradeDetailById(Guid id)
        {
            try
            {
                var gradeDetail = await _gradeService.GetGradeDetailByIdAsync(id);
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

        [HttpPost("details")]
        public async Task<IActionResult> CreateGradeDetail([FromBody] GradeDetail gradeDetail)
        {
            try
            {
                await _gradeService.AddGradeDetailAsync(gradeDetail);
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

        [HttpPut("details/{id}")]
        public async Task<IActionResult> UpdateGradeDetail(Guid id, [FromBody] GradeDetail gradeDetail)
        {
            if (id != gradeDetail.GradeDetailID)
                return BadRequest("GradeDetail ID mismatch");

            try
            {
                await _gradeService.UpdateGradeDetailAsync(gradeDetail);
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

        [HttpDelete("details/{id}")]
        public async Task<IActionResult> DeleteGradeDetail(Guid id)
        {
            try
            {
                await _gradeService.DeleteGradeDetailAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
