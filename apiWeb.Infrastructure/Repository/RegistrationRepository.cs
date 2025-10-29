using apiWeb.Domain.Interfaces;
using apiWeb.Domain.Models;
using apiWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace apiWeb.Infrastructure.Repository;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly AppDbContext  _context;
    public RegistrationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Registration>> GetAllRegistrationsAsync()
    {
        return await _context.Registrations
            .Include(r => r.Student)
            .Include(r => r.Course)
            .ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync(int studentId)
    {
        return await _context.Registrations
            .Where(r => r.StudentId == studentId)
            .Include(r => r.Course)
            .Select(r => r.Course!)
            .ToListAsync();
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync(int courseId)
    {
        return await _context.Registrations
            .Where(r => r.CourseId == courseId)
            .Include(r => r.Student)
            .Select(r => r.Student!)
            .ToListAsync();
    }

    public async Task CreateRegistrationAsync(Registration registration)
    {
        await _context.Registrations.AddAsync(registration);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRegistrationAsync(Registration registration)
    {
        _context.Registrations.Update(registration);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteRegistrationAsync(int id)
    {
        var registration = await _context.Registrations.FindAsync(id);
        if (registration != null)
        {
            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
        }
    }
}