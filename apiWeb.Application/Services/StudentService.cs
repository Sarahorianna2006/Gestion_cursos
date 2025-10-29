using apiWeb.Application.DTOs;
using apiWeb.Domain.Interfaces;
using apiWeb.Domain.Models;

namespace apiWeb.Application.Services;

public class StudentService
{
    private readonly IStudentRepository  _studentRepository;

    public StudentService(IStudentRepository context)
    {
        _studentRepository = context;
    }
    
    public async Task<StudentDetailsDto?> GetStudentDetailsAsync(int id)
    {
        // Obtenemos el estudiante junto con sus registros y cursos
        var student = await _studentRepository.GetStudentByIdWithRegistrationsAsync(id);
        if (student == null)
            return null;

        // Convertimos los datos al DTO limpio
        return new StudentDetailsDto
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age,
            Email = student.Email,
            Courses = student.Registrations?
                .Select(r => new CourseDto
                {
                    Id = r.Course?.Id ?? 0,
                    Title = r.Course?.Title ?? "",
                    Description = r.Course?.Description ?? ""
                }).ToList()
        };
    }

    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllStudentsAsync();
        return students.Select(s => new StudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Age = s.Age,
            Email = s.Email
        }).ToList();
    }

    public async Task<StudentDto?> GetStudentByIdAsync(int id)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);
        if (student == null)
            return null;

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Age = student.Age,
            Email = student.Email
        };
    }

    public async Task CreateStudentAsync(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Name))
            throw new ArgumentNullException("Student name is required");
        
        await _studentRepository.CreateStudentAsync(student);
    }

    public async Task UpdateStudentAsync(Student student)
    {
        var existingStudent = await _studentRepository.GetStudentByIdAsync(student.Id);
        if (existingStudent == null)
            throw new InvalidOperationException("Student not found");
       
        await _studentRepository.UpdateStudentAsync(student); 
    }

    public async Task DeleteStudentAsync(int id)
    {
        var existingStudent = await _studentRepository.GetStudentByIdAsync(id);
        if (existingStudent == null)
            throw new InvalidOperationException("Cannot delete: student does not exist");
        
        await _studentRepository.DeleteStudentAsync(id);
    }
}