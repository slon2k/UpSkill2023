using FileGenerator;

namespace ConsoleApp;

internal class Student
{

    [CsvGenerator(Format = "00")]
    public int Id { get; set; }

    [CsvGenerator(Heading = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [CsvGenerator(Heading = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [CsvGenerator(Heading = "Date of Birth", Format = "dd.MM.yyyy")]
    public DateTime BirthDate { get; set; }

    internal static IEnumerable<Student> GenerateSampleData() => new List<Student>()
    {
        new Student{ Id = 1, FirstName = "Harry", LastName = "Potter", BirthDate = new DateTime(1980,07,31)},
        new Student{ Id = 2, FirstName = "Hermione", LastName = "Granger", BirthDate = new DateTime(1979,9,19)},
    };
}
