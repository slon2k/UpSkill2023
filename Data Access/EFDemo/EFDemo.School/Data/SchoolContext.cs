using EFDemo.School.Models;
using Microsoft.EntityFrameworkCore;

namespace EFDemo.School.Data;

public class SchoolContext : DbContext
{
    public DbSet<House> Houses { get; set; }
    
    public DbSet<Course> Courses { get; set; }
    
    public DbSet<Student> Students { get; set; }
    
    public DbSet<CourseDetails> CourseDetails { get; set; }

    public DbSet<Enrollment> Enrollments { get; set; }

    public SchoolContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Enrollment>(entity => {
            entity
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            entity
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasKey(e => new { e.StudentId, e.CourseId } );
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity
                .HasIndex(c => c.Code)
                .IsUnique();

        });

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Courses)
            .WithMany(c => c.Students)
            .UsingEntity<Enrollment>();

        base.OnModelCreating(modelBuilder);
    }
}
