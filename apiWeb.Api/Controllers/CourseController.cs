using apiWeb.Application.Services;
using apiWeb.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;
        
        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllCoursesAsync()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCourseByIdAsync(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if  (course == null)
                return NotFound("Course not found");
            
            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCourseAsync([FromBody] Course course)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            await _courseService.CreateCourseAsync(course);
            return Ok(course);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourseAsync(int id, [FromBody] Course course)
        {
            if (id != course.Id)
                return BadRequest("Course ID does not match");
            
            await _courseService.UpdateCourseAsync(course);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourseAsync(int id)
        {
            await _courseService.DeleteCourseAsync(id);
            return NoContent();
        }
    }