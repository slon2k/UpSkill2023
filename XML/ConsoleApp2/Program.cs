using ConsoleApp2;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Files");

string customerFilePath = Path.Combine(folderPath, "Customer.json");
string customersFilePath = Path.Combine(folderPath, "Customers.json");

string customerJsonText = File.ReadAllText(customerFilePath);

var stopwatch = new Stopwatch();

stopwatch.Start();

var customer = JsonSerializer.Deserialize<Customer>(customerJsonText);
var customerJson = JsonSerializer.Serialize(customer, new JsonSerializerOptions { WriteIndented = true });

stopwatch.Stop();

Console.WriteLine(customerJson);
Console.WriteLine($"Reflection: {stopwatch.ElapsedMilliseconds}");

stopwatch.Restart();

var customerSourceGenerated = JsonSerializer.Deserialize(customerJsonText, typeof(Customer), CustomerSerializerContext.Default);
var customerSourceGeneratedJson = JsonSerializer.Serialize(customerSourceGenerated, typeof(Customer), CustomerSerializerContext.Default);

stopwatch.Stop();

Console.WriteLine(customerSourceGeneratedJson);
Console.WriteLine($"Source generation: {stopwatch.ElapsedMilliseconds}");

using var ms = new MemoryStream();
using var writer = new Utf8JsonWriter(ms, new JsonWriterOptions { Indented = true });

writer.WriteStartObject();
writer.WriteString("name", "Harry");
writer.WriteBoolean("active", true);
writer.WritePropertyName("course");
writer.WriteStartArray();
writer.WriteStartObject();
writer.WriteString("name", "History of Magic");
writer.WriteNumber("grade", 4);
writer.WriteEndObject();
writer.WriteStartObject();
writer.WriteString("name", "Transfiguration");
writer.WriteNumber("grade", 5);
writer.WriteEndObject();
writer.WriteEndArray();
writer.WriteEndObject();

writer.Flush();

string json = Encoding.UTF8.GetString(ms.ToArray());
Console.WriteLine(json);

string order = File.ReadAllText(Path.Combine(folderPath, "SalesOrder.json"));

JsonDocument document = JsonDocument.Parse(order);

JsonElement root = document.RootElement;

JsonElement details = root.GetProperty("OrderDetails");

decimal total = 0;

foreach (var element in details.EnumerateArray())
{
	if (element.TryGetProperty("LineTotal", out JsonElement lineTotalElement))
	{
		if (lineTotalElement.TryGetDecimal(out decimal lineTotal))
		{
			total += lineTotal;
		}
	}
}

Console.WriteLine($"Total: {total:0,0.00}");

Console.WriteLine("\nHello, World!");
Console.ReadKey();