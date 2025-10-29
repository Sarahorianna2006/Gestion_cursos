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

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        return await _studentRepository.GetAllStudentsAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int id)
    {
        return await _studentRepository.GetStudentByIdAsync(id);
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