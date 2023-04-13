using System.Text.Json.Serialization;

namespace ConsoleApp2;

[JsonSerializable(typeof(Customer))]
[JsonSerializable(typeof(List<Customer>))]
[JsonSourceGenerationOptions(WriteIndented = true, PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
internal partial class CustomerSerializerContext : JsonSerializerContext
{
}
