namespace AdoDemoApp;

public record HouseModel(int Id, string Name, IEnumerable<StudentModel> Students);

public record StudentModel(int Id, string Name, string House);