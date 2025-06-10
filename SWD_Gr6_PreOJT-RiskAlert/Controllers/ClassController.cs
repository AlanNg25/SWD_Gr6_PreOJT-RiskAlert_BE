//using Microsoft.AspNetCore.Mvc;
//using Repositories.Models;
//using Services.Interface;

//namespace GenderHealthcare.API.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ClassesController : ControllerBase
//    {
//        private readonly IClassService _classService;

//        public ClassesController(IClassService classService)
//        {
//            _classService = classService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<List<Classes>>> GetAllClasses()
//        {
//            var result = await _classService.GetAllClassesAsync();
//            return Ok(result);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Classes>> GetClassById(Guid id)
//        {
//            var result = await _classService.GetClassByIdAsync(id);
//            if (result == null || result.ClassID == Guid.Empty)
//                return NotFound();

//            return Ok(result);
//        }

//        [HttpPost]
//        public async Task<ActionResult> CreateClass([FromBody] Classes classes)
//        {
//            var rowsAffected = await _classService.CreateClassAsync(classes);
//            if (rowsAffected > 0)
//                return CreatedAtAction(nameof(GetClassById), new { id = classes.ClassID }, classes);

//            return BadRequest("Creation failed.");
//        }

//        [HttpPut("{id}")]
//        public async Task<ActionResult> UpdateClass(Guid id, [FromBody] Classes classes)
//        {
//            if (id != classes.ClassID)
//                return BadRequest("ID mismatch.");

//            var rowsAffected = await _classService.UpdateClassAsync(classes);
//            if (rowsAffected > 0)
//                return NoContent();

//            return NotFound();
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult> DeleteClass(Guid id)
//        {
//            var rowsAffected = await _classService.DeleteClassAsync(id);
//            if (rowsAffected > 0)
//                return NoContent();

//            return NotFound();
//        }

//        [HttpGet("search")]
//        public async Task<ActionResult<List<Classes>>> SearchClass([FromQuery] string classCode, [FromQuery] string className)
//        {
//            var result = await _classService.SearchClassAsync(classCode, className);
//            return Ok(result);
//        }
//    }
//}
