using System.ComponentModel.DataAnnotations;

namespace EFDemo.School.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    public string Code { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;

    public CourseDetails? CourseDetails { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }

    public ICollection<Student> Students { get; set; }
}
