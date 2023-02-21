namespace AdoDemoApp.Models;

public class House
{
    public House(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; }
}
