using DemoConsoleApp;
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = config.GetConnectionString("DbConnection") ?? throw new InvalidOperationException("Unable to load connection string");

var dbInitializer = new DbInitializer(connectionString);

dbInitializer.CreateTables();

Console.WriteLine(connectionString);
