using AdoDemoApp.Data;
using AdoDemoApp.Interfaces;
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

var houseService = new HouseService(connectionString);

var houses = houseService.GetHousesWithStudents();

Console.WriteLine();

foreach (var house in houses)
{
    Console.WriteLine();
    Console.WriteLine("-----------------");
    Console.WriteLine($"{house.Name}");
    Console.WriteLine("-----------------");
    foreach (var student in house.Students)
    {
        Console.WriteLine(student.Name);
    }
    Console.WriteLine("-----------------");
}


Console.WriteLine();
Console.WriteLine("Press any key to quit");
Console.ReadKey();
