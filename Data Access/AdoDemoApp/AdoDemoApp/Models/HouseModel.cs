namespace AdoDemoApp.Models;

public record HouseModel(int Id, string Name, IEnumerable<StudentModel> Students);
