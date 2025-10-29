using apiWeb.Application.DTOs;
using apiWeb.Application.Services;
using apiWeb.Domain.Interfaces;
using apiWeb.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace apiWeb.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly RegistrationService  _registrationService;
    
    public  RegistrationController(RegistrationService registrationService)
    {
        _registrationService = registrationService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllRegistrationsAsync()
    {
        var registrations = await _registrationService.GetAllRegistrationsAsync();
        return Ok(registrations);
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetCoursesByStudentAsync(int studentId)
    {
        var courses = await _registrationService.GetAllCoursesAsync(studentId);
        return Ok(courses);
    }

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetStudentsByCourseAsync(int courseId)
    {
        var students = await _registrationService.GetAllStudentsAsync(courseId);
        return Ok(students);
    }

    [HttpPost]
    public async Task<ActionResult> CreateRegistrationAsync([FromBody] RegistrationDto dto)
    {
        var registration = new Registration
        {
            StudentId = dto.StudentId,
            CourseId = dto.CourseId
        };

        await _registrationService.CreateRegistrationAsync(registration);
        return Ok(registration);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateRegistrationAsync(int id, [FromBody] RegistrationDto dto)
    {
        if (id != dto.Id)
            return BadRequest("Registration ID mismatch");

        var registration = new Registration
        {
            Id = dto.Id,
            StudentId = dto.StudentId,
            CourseId = dto.CourseId
        };

        await _registrationService.UpdateRegistrationAsync(registration);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteRegistrationAsync(int id)
    {
        await _registrationService.DeleteRegistrationAsync(id);
        return NoContent();
    }
}