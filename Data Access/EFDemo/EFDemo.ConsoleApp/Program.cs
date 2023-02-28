using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Northwind.Data;
using Northwind.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var connectionString = config.GetConnectionString("Default") ?? throw new InvalidOperationException();

using var context = new NorthwindContext(connectionString);

//var orders = context.Orders
//    .Take(5)
//    .Include(o => o.Employee)
//    .Include(o => o.Customer);

var orderRepo = new OrderRepository(context);

var orders = orderRepo.GetAll().Take(5);

var options = new JsonSerializerOptions
{
    WriteIndented= true,
    ReferenceHandler = ReferenceHandler.IgnoreCycles
};

foreach (var order in orders)
{
    Console.WriteLine(JsonSerializer.Serialize(order, options));
}

Console.WriteLine(connectionString);
Console.ReadKey();