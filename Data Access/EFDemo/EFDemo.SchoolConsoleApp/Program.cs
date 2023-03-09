using EFDemo.School.Data;
using EFDemo.School.Models;
using EFDemo.SchoolConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = config.GetConnectionString("Default") ?? throw new InvalidOperationException();

var dbOptions = new DbContextOptionsBuilder<SchoolContext>()
    .UseSqlServer(connectionString)
    .Options;

using var context = new SchoolContext(dbOptions);

context.Database.EnsureDeleted();
context.Database.EnsureCreated();

context.Houses.AddRange(SchoolData.GetHouses());
context.Courses.AddRange(SchoolData.GetCourses());

context.SaveChanges();

var random = new Random();

var query =
    from student in context.Students.Take(10)
    from course in context.Courses.Skip(1).Take(5)
    select new Enrollment { Course = course, Student = student, DateStart = DateTime.Now.AddDays(-random.Next(30)) };

context.Enrollments.AddRange(query.ToList());

context.SaveChanges();

var students = context.Students
    .Include(s => s.House)
    .Include(s => s.Courses)
        .ThenInclude(c => c.CourseDetails)
    .Take(10)
    .ToList();

foreach (var student in students)
{
    Console.WriteLine("-------------------------------------");
    Console.WriteLine($"{student.FirstName} {student.LastName} from {student.House.Name}");
    Console.WriteLine("-------------------------------------");
    foreach (var course in student.Courses)
    {
        Console.WriteLine($"{course.Name}:\n{course.CourseDetails?.Description}");
    }
    Console.WriteLine("-------------------------------------\n");
}


Console.WriteLine("Press a key to quit");
Console.ReadKey();
