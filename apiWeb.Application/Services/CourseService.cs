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

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _context.GetAllCoursesAsync();
    }
    
    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await _context.GetCourseByIdAsync(id);
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