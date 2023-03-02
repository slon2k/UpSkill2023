namespace EFDemo.School.Models;

public class CourseDetails
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public string Description { get; set; } = null!;

    public Course Course { get; set; } = null!;
}
