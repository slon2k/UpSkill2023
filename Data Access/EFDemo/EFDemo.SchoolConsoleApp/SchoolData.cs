using EFDemo.School.Models;

namespace EFDemo.SchoolConsoleApp;

internal static class SchoolData
{
    private static readonly string coursesFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "courses.csv");
    
    private static readonly string studentsFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "students.csv");

    internal static IEnumerable<House> GetHouses() =>
        File.ReadAllLines(studentsFilePath)
            .Skip(1)
            .Where(l => l.Length > 0)
            .Select(l => l.Split(','))
            .Select(r => new { Name = r[0], House = r[1] })
            .GroupBy(r => r.House)
            .Select(g => new House
            {
                Name = g.Key,
                Students = g.Select(x => new Student
                {
                    FirstName = x.Name.Split(' ').First(),
                    LastName = x.Name.Split(' ').Last()
                }).ToList()
            });

    internal static IEnumerable<Course> GetCourses() =>
        File.ReadAllLines(coursesFilePath)
            .Skip(1)
            .Where(l => l.Length > 0)
            .Select(l => l.Split(";"))
            .Select(r => new { Name = r[0], Code = r[1], Description = r[2] })
            .Select(l => new Course
            {
                Name = l.Name,
                Code = l.Code,
                CourseDetails = new CourseDetails { Description = l.Description }
            });
}
