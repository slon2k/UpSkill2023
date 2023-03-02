namespace EFDemo.School.Models;

public class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int HouseId { get; set; }

    public House House { get; set; } = null!;

    public ICollection<Enrollment> Enrollments { get; set; }

    public ICollection<Course> Courses { get; set; }
}
