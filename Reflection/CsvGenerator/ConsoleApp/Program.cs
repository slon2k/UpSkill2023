using ConsoleApp;
using FileGenerator;

var generator = new CsvGenerator(';');

var students = Student.GenerateSampleData();

var cars = Car.GenerateSampleData();

generator.GenerateFile(students, Path.Combine(Directory.GetCurrentDirectory(), "students.csv"));

Console.WriteLine();
generator.GenerateFile(cars, Path.Combine(Directory.GetCurrentDirectory(), "cars.csv"));

Console.WriteLine("Press a key to quit");
Console.ReadKey();
