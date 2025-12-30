using Microsoft.AspNetCore.Mvc;
using StudentManagement.BLL.DTOs;
using StudentManagement.BLL.Services;

namespace StudentManagement.App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _service;

        public StudentsController(StudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<StudentDTO>> GetAll()
        {
            var students = _service.GetAll();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<StudentDTO> GetById(int id)
        {
            var student = _service.GetById(id);
            if (student == null)
                return NotFound();
            
            return Ok(student);
        }

        [HttpPost]
        public ActionResult Create([FromBody] StudentDTO studentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Create(studentDto);
            return Ok("Student created successfully");
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] StudentDTO studentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            studentDto.StudentId = id;
            _service.Update(studentDto);
            return Ok("Student updated successfully");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("Student deleted successfully");
        }
    }
}
