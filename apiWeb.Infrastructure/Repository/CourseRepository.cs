using apiWeb.Domain.Interfaces;
using apiWeb.Domain.Models;
using apiWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace apiWeb.Infrastructure.Repository;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;
    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course?> GetCourseByIdAsync(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public async Task CreateCourseAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCourseAsync(Course course)
    {
        var existingCourse = await _context.Courses.FindAsync(course.Id);
        if (existingCourse == null)
            throw new InvalidOperationException("Course not found");

        _context.Entry(existingCourse).CurrentValues.SetValues(course);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCourseAsync(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course != null)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}