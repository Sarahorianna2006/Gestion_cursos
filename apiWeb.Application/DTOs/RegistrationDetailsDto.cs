namespace apiWeb.Application.DTOs;

public class RegistrationDetailsDto
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public int CourseId { get; set; }
    public string? CourseTitle { get; set; }
}