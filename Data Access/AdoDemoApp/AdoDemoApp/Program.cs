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

var dbType = config.GetValue<string>("dbType") ?? throw new InvalidOperationException("Unsupported DB type");
var connectionString = config.GetConnectionString(dbType) ?? throw new InvalidOperationException("Unable to load the connection string");

IDbConnectionFactory dbConnectionFactory = dbType == "Postgres" ? 
    new PgDbConnectionFactory(connectionString) : 
    new SqlDbConnectionFactory(connectionString);

var dbInitializer = dbConnectionFactory.CreateDbInitializer();

dbInitializer.DropTables();
dbInitializer.CreateTables();
dbInitializer.SeedData();

var studentService = new StudentService(dbConnectionFactory);

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

