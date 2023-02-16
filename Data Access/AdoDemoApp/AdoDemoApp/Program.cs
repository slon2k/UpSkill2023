using AdoDemoApp;
using DemoConsoleApp;
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
} else {
    connectionString = config.GetConnectionString("DbConnection") ?? throw new InvalidOperationException("Unable to load connection string");
    dbInitializer = new DbInitializer(connectionString);
}

dbInitializer.CreateTables();

Console.WriteLine("----------------");
