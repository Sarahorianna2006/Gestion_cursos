using apiWeb.Application.DTOs;
using apiWeb.Domain.Interfaces;
using apiWeb.Domain.Models;

namespace apiWeb.Application.Services;

public class CourseService
{
    private readonly ICourseRepository _context;
    
    public CourseService(ICourseRepository context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
    {
        var courses = await _context.GetAllCoursesAsync();
        return courses.Select(c => new CourseDto
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description
        }).ToList();
    }
    
    public async Task<CourseDto?> GetCourseByIdAsync(int id)
    {
        var course = await _context.GetCourseByIdAsync(id);
        if (course == null)
            return null;

        return new CourseDto
        {
            Id = course.Id,
            Title = course.Title,
            Description = course.Description
        };
    }

    public async Task CreateCourseAsync(Course course)
    {
        if (string.IsNullOrWhiteSpace(course.Title))
            throw new ArgumentNullException("Course title required");
        
        await _context.CreateCourseAsync(course);
    }

    public async Task UpdateCourseAsync(Course course)
    {
        var existingCourse = await _context.GetCourseByIdAsync(course.Id);
        if (existingCourse == null)
            throw new ArgumentNullException("Course not found");
        
        await _context.UpdateCourseAsync(course);
    }
    
    public async Task DeleteCourseAsync(int id)
    {
        var existingCourse = await _context.GetCourseByIdAsync(id);
        if (existingCourse == null)
            throw new ArgumentNullException("Cannot delete: course does not exist");

        await _context.DeleteCourseAsync(id);
    }
}