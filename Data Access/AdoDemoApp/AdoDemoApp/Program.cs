using AdoDemoApp.Data;
using AdoDemoApp.Extensions;
using AdoDemoApp.Interfaces;
using AdoDemoApp.Models;
using AdoDemoApp.Services;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddCommandLine(args)
    .Build();

IDbInitializer dbInitializer;
string connectionString;

var dbType = config.GetValue<string>("db");

if (dbType == "postgres")
{
    connectionString = config.GetConnectionString("PgConnection") ?? throw new InvalidOperationException("Unable to load connection string");
    dbInitializer = new PgDbInitializer(connectionString);
} 
else 
{
    connectionString = config.GetConnectionString("DbConnection") ?? throw new InvalidOperationException("Unable to load connection string");
    dbInitializer = new DbInitializer(connectionString);
}

dbInitializer.DropTables();
dbInitializer.CreateTables();
dbInitializer.SeedData();

var studentService = new StudentService(new SqlDbConnectionFactory(connectionString));

studentService.Create(new Student("Bill", "Weasley", 1));
studentService.Create(new Student("Ginny", "Weasley", 1));
studentService.Create(new Student("Fred", "Weasley", 1));

studentService.Update(new Student(13, "George", "Weasley", 1));

var houseService = new HouseService(connectionString);

var houses = houseService.GetHousesWithStudents();

houses.Print();

Console.WriteLine();
Console.WriteLine("Press any key to quit");
Console.ReadKey();

