using Microsoft.AspNetCore.Mvc;
using StudentManagement.DTOs;
using StudentManagement.Services.Implementations;
using StudentManagement.Services.Interfaces;

namespace StudentManagement.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _service;

        public CoursesController(ICourseService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            await _service.CreateCourseAsync(dto);
            return Ok("Course created");
        }
        
        [HttpPost("enroll")]
        public async Task<IActionResult> Enroll(EnrollStudentDto dto)
        {
            await _service.EnrollStudentAsync(dto);
            return Ok("Student enrolled");
        }

        [HttpGet("students-with-courses")]
        public async Task<IActionResult> GetStudentsWithCourses()
        {
            var data = await _service.GetStudentsWithCourses();
            return Ok(data);
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var data = await _service.GetCourseStats();
            return Ok(data);
        }

        [HttpGet("students-by-course")]
        public async Task<IActionResult> GetByCourse(string courseName)
        {
            var data = await _service.GetStudentsByCourse(courseName);
            return Ok(data);
        }
    }
}
