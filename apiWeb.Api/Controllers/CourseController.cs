using apiWeb.Application.DTOs;
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
        public async Task<ActionResult> CreateCourseAsync([FromBody] CourseDto courseDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var course = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description
            };

            await _courseService.CreateCourseAsync(course);
            return Ok(course);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourseAsync(int id, [FromBody] CourseDto courseDto)
        {
            if (id != courseDto.Id)
                return BadRequest("Course ID does not match");

            var course = new Course
            {
                Id = courseDto.Id,
                Title = courseDto.Title,
                Description = courseDto.Description
            };

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