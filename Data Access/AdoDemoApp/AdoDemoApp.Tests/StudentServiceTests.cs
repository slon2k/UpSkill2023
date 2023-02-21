using AdoDemoApp.Interfaces;
using AdoDemoApp.Services;
using Moq;
using System.Data;

namespace AdoDemoApp.Tests;

public class StudentServiceTests
{
    [Fact]
    public void GetStudents_Returns_StudentList()
    {
        var readerMock = new Mock<IDataReader>();
        
        readerMock.SetupSequence(r => r.Read())
            .Returns(true)
            .Returns(true)
            .Returns(false);

        readerMock.SetupSequence(r => r.GetInt32(0)).Returns(3).Returns(4);
        readerMock.SetupSequence(r => r.GetInt32(1)).Returns(4).Returns(1);
        readerMock.SetupSequence(r => r.GetString(2)).Returns("FirstName1").Returns("FirstName2");
        readerMock.SetupSequence(r => r.GetString(3)).Returns("LastName1").Returns("LastName2");

        var commandMock = new Mock<IDbCommand>();
        commandMock.Setup(c => c.ExecuteReader()).Returns(readerMock.Object);

        var connectionMock = new Mock<IDbConnection>();
        connectionMock.Setup(c => c.CreateCommand()).Returns(commandMock.Object);

        var connectionFactoryMock =  new Mock<IDbConnectionFactory>();
        connectionFactoryMock.Setup(f => f.CreateConnection()).Returns(connectionMock.Object);

        var studentService = new StudentService(connectionFactoryMock.Object);

        var students = studentService.GetAll();

        Assert.NotNull(students);
        Assert.Equal(2, students.Count());

        var student1 = students.First();
        Assert.NotNull(student1);
        Assert.Equal("FirstName1", student1.FirstName);
        
        var student2 = students.Last();
        Assert.NotNull(student2);
        Assert.Equal("FirstName2", student2.FirstName);
        
        commandMock.Verify(c => c.ExecuteReader(), Times.Once);
        commandMock.VerifySet(c => c.CommandText = StoredProcedures.Students.GetAll, Times.Once);
        commandMock.VerifySet(c => c.CommandType = CommandType.StoredProcedure, Times.Once);
    }
}
