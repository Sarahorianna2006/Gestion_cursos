using apiWeb.Domain.Models;

namespace apiWeb.Domain.Interfaces;

public interface IRegistrationRepository
{
    Task<IEnumerable<Registration>> GetAllRegistrationsAsync();
    Task<IEnumerable<Course>> GetAllCoursesAsync(int studentId); //obtener curso por estudiante
    Task<IEnumerable<Student>> GetAllStudentsAsync(int courseId); //obtener estudiante por curso
    Task CreateRegistrationAsync(Registration registration);
    Task UpdateRegistrationAsync(Registration registration);
    Task DeleteRegistrationAsync(int id);
}