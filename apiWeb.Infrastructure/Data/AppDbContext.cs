using apiWeb.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace apiWeb.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions options) : base(options){}
    
    public DbSet<Student>  Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Registration> Registrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Registration>().HasKey(i => i.Id);
        
        modelBuilder.Entity<Registration>().HasOne(i => i.Student)
            .WithMany(s => s.Registrations)
            .HasForeignKey(i => i.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Registration>()
            .HasOne(i => i.Course)
            .WithMany(c => c.Registrations)
            .HasForeignKey(i => i.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}