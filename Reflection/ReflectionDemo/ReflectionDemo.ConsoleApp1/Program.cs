using ReflectionDemo.ConsoleApp1;
using System.Reflection;

var hello = "Hello";

var type2 = hello.GetType();
var type1 = typeof(string);

var assembly = Assembly.GetExecutingAssembly();

//Console.WriteLine("Types: ");
//foreach (var type in assembly.GetTypes())
//{
//    Console.WriteLine(type.Name);
//}

//var extAssembly = Assembly.Load("System.Text.Json");

//Console.WriteLine("\nTypes: ");
//foreach (var type in extAssembly.GetTypes())
//{
//    Console.WriteLine(type.Name);
//}

//var personType = typeof(Person);

var personType = assembly.GetType("ReflectionDemo.ConsoleApp1.Person");

var methods = personType?.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

Console.WriteLine();
Console.WriteLine("Methods: ");

foreach (var method in methods)
{
    Console.WriteLine(method.Name);
}

var props = personType?.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

Console.WriteLine();
Console.WriteLine("Properties: ");

foreach (var p in props)
{
    Console.WriteLine(p.Name);
}

var fields = personType?.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

Console.WriteLine();
Console.WriteLine("Fields: ");

foreach (var field in fields)
{
    Console.WriteLine(field.Name);
}

var person = new Person { FirstName= "First", LastName="Last" };

var secretField = personType?.GetField("secret", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

Console.WriteLine();
Console.WriteLine("Secret:");
Console.WriteLine(secretField?.GetValue(person));

var idProp = personType?.GetProperty("Id", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
idProp?.SetValue(person, 42);

Console.WriteLine();
Console.WriteLine("Id:");
Console.WriteLine(person.Id);

var privateMethod = personType?.GetMethod("PrivateMethod", BindingFlags.Instance | BindingFlags.NonPublic);

Console.WriteLine();
Console.WriteLine(privateMethod?.Name);
privateMethod?.Invoke(person, new object[] { });

var countProp = personType.GetProperty("Count", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

Console.WriteLine();
Console.WriteLine(countProp?.Name);
Console.WriteLine(countProp?.GetValue(person));

//this doesn't work without a setter:
//countProp.SetValue(person, 42);

var backingField = personType.GetField("<Count>k__BackingField", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
backingField?.SetValue(person, 2023);

Console.WriteLine("After update:");
Console.WriteLine(countProp?.GetValue(person));


Console.WriteLine();
Console.WriteLine("Press a key");
Console.ReadKey();
