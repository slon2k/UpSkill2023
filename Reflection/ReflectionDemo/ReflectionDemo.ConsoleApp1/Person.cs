namespace ReflectionDemo.ConsoleApp1;

internal class Person
{
    private readonly string secret = Guid.NewGuid().ToString();

    public int Id { get; private set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int Count { get; } = 1;

    private void PrivateMethod() => Console.WriteLine("From private method");
}
