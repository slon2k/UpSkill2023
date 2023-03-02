namespace EFDemo.School.Models;

public class Enrollment
{
    public int CourseId { get; set; }

    public int StudentId { get; set; }

    public int Grade { get; set; }

    public DateTime DateStart { get; set; }

    public DateTime? DateEnd { get; set; }

    public Student Student { get; set; } = null!;

    public Course Course { get; set; } = null!;
}
