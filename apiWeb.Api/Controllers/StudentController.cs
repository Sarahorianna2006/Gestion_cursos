using apiWeb.Application.Services;
using apiWeb.Domain.Models;
using apiWeb.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace apiWeb.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly StudentService _studentService;
    
    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudentsAsync()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentByIdAsync(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        if  (student == null)
            return NotFound("Student not found");
        
        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudentAsync([FromBody] Student student)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _studentService.CreateStudentAsync(student);
        
        return Ok(student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudentAsync(int id, [FromBody] Student student)
    {
        if (id != student.Id)
            return BadRequest("Student ID does not match");

        await _studentService.UpdateStudentAsync(student);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudentAsync(int id)
    {
        await _studentService.DeleteStudentAsync(id);
        return NoContent();
    }
}