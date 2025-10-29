using apiWeb.Application.DTOs;
using apiWeb.Domain.Interfaces;
using apiWeb.Domain.Models;

namespace apiWeb.Application.Services;

public class RegistrationService
{
    private readonly IRegistrationRepository _registrationRepository;
    
    public RegistrationService(IRegistrationRepository registrationRepository)
    {
        _registrationRepository = registrationRepository;
    }

    public async Task<IEnumerable<RegistrationDetailsDto>> GetAllRegistrationsAsync()
    {
        var registrations = await _registrationRepository.GetAllRegistrationsAsync();

        return registrations.Select(r => new RegistrationDetailsDto
        {
            Id = r.Id,
            StudentId = r.StudentId,
            StudentName = r.Student?.Name,
            CourseId = r.CourseId,
            CourseTitle = r.Course?.Title
        }).ToList();
    }
    
    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync(int studentId)
    {
        var courses = await _registrationRepository.GetAllCoursesAsync(studentId);

        return courses.Select(c => new CourseDto
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description
        }).ToList();;
    }

    public async Task<IEnumerable<StudentDto>> GetAllStudentsAsync(int courseId)
    {
        var students = await _registrationRepository.GetAllStudentsAsync(courseId);

        return students.Select(s => new StudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Age = s.Age,
            Email = s.Email
        }).ToList();
    }
    
    public async Task CreateRegistrationAsync(Registration registration)
    {
        if (registration.StudentId <= 0 || registration.CourseId <= 0)
            throw new ArgumentException("StudentId and CourseId are required");
        
        await _registrationRepository.CreateRegistrationAsync(registration);
    }

    public async Task UpdateRegistrationAsync(Registration registration)
    {
        await _registrationRepository.UpdateRegistrationAsync(registration);
    }

    public async Task DeleteRegistrationAsync(int id)
    {
        await _registrationRepository.DeleteRegistrationAsync(id);
    }
}