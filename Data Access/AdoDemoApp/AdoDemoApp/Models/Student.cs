namespace AdoDemoApp.Models;

public class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int HouseId { get; set; }
    
    public Student(int id, string firstName, string lastName, int houseId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        HouseId = houseId;
    }

    public Student(string firstName, string lastName, int houseId)
    {
        FirstName = firstName;
        LastName = lastName;
        HouseId = houseId;
    }
}
