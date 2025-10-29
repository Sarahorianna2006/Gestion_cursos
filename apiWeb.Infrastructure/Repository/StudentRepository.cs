using apiWeb.Domain.Interfaces;
using apiWeb.Domain.Models;
using apiWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace apiWeb.Infrastructure.Repository;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;
    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _context.Students
            .Include(s => s.Registrations)
            .ThenInclude(r => r.Course)
            .ToListAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await _context.Students
            .Include(s => s.Registrations)
            .ThenInclude(r => r.Course)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task CreateStudentAsync(Student student)
    {
        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateStudentAsync(Student student)
    {
        _context.Students.Update(student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
    }
}