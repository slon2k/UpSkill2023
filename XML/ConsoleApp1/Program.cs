using ConsoleApp1;
using ConsoleApp1.Models;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

var path = Path.Combine(Directory.GetCurrentDirectory(), "xml");
var customerPath = Path.Combine(path, "Customer_IDAttribute.xml");
var customersPath = Path.Combine(path, "Customers_IDAttribute.xml");

var serializer = new XmlSerializer(typeof(Customer));

var serializerCustomers = new XmlSerializer(typeof(List<Customer>), new XmlRootAttribute("Customers"));

using (var fs = new FileStream(customerPath, FileMode.Open))
{
	try
	{
		Customer customer = (Customer)serializer.Deserialize(fs);
		
		Console.WriteLine(customer.ToString());

		var jsonCustomer = JsonSerializer.Serialize(customer, new JsonSerializerOptions { WriteIndented = true });

		Console.WriteLine(jsonCustomer.ToString());

		var fileName = Path.ChangeExtension(customerPath, "json");

		File.WriteAllText(fileName, jsonCustomer);
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
}

using (var fs = new FileStream(customersPath, FileMode.Open))
{
	try
	{
        List<Customer> customers = (List<Customer>)serializerCustomers.Deserialize(fs);
		
		foreach (var customer in customers)
		{
			Console.WriteLine(customer.ToString());
		}

		var jsonCustomers = JsonSerializer.Serialize(
				customers,
				new JsonSerializerOptions 
				{ 
					WriteIndented = true,
					PropertyNamingPolicy = JsonNamingPolicy.CamelCase
				});

		Console.WriteLine(jsonCustomers.ToString());
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
}

Console.WriteLine();

using (var sw = new StringWriter())
{
	serializer.Serialize(sw, new Customer { Id = 42, FirstName = "Harry", LastName = "Potter" });

	var result = sw.ToString();

	Console.WriteLine();
	Console.WriteLine(result);
}

Console.WriteLine();

using (var sw = new Utf8StringWriter())
{
    serializerCustomers.Serialize(sw, new List<Customer> 
	{
        new Customer { Id = 42, FirstName = "Harry", LastName = "Potter" },
        new Customer { Id = 40, FirstName = "Hermione", LastName = "Granger" }
    });

	var result = sw.ToString();

	Console.WriteLine();
	Console.WriteLine(result);
}

Console.WriteLine();

var doc = new XmlDocument();

doc.Load(customersPath);
doc.PreserveWhitespace = true;

var root = doc.DocumentElement;

XmlNodeList nodeList = root.SelectNodes("/Customers/Customer");

foreach (var node in nodeList)
{
	var element = node as XmlElement;
	Console.WriteLine($"{element["FirstName"].InnerText} {element["LastName"].InnerText} {element["CompanyName"].InnerText}"); 
}

XElement rootElement = XElement.Load(customersPath);

var query =
	from element in rootElement.Elements("Customer")
	select element;

foreach (var item in query)
{
	Console.WriteLine();
	
	Console.WriteLine(item.Attribute("customerId")?.Value);
	
	foreach (var element in item.Elements())
	{
		Console.WriteLine($"{element.Name} {element.Value}");
	}

}

Console.WriteLine();

var names = rootElement.Elements("Customer").Select(e => e.Element("FirstName"));

foreach (var item in names)
{
	Console.WriteLine(item?.Value);
}

Console.WriteLine("Press a key to quit");
Console.ReadKey();
