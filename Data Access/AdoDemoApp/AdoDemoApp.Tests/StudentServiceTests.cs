using AdoDemoApp.Interfaces;
using AdoDemoApp.Models;
using AdoDemoApp.Services;
using Moq;
using System.Data;
using System.Data.SqlClient;

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

    [Fact]
    public void GetStudent_Returns_Student()
    {
        var readerMock = new Mock<IDataReader>();

        readerMock.SetupSequence(reader => reader.Read())
            .Returns(true)
            .Returns(false);

        readerMock.Setup(reader => reader.GetInt32(0)).Returns(3);
        readerMock.Setup(reader => reader.GetInt32(1)).Returns(2);
        readerMock.Setup(reader => reader.GetString(2)).Returns("FirstName");
        readerMock.Setup(reader => reader.GetString(3)).Returns("LastName");

        var parameterCollectionMock = new Mock<IDataParameterCollection>();

        var commandMock = new Mock<IDbCommand>();
        commandMock.Setup(command => command.ExecuteReader()).Returns(readerMock.Object);
        commandMock.Setup(command => command.Parameters).Returns(parameterCollectionMock.Object);

        var connectionMock = new Mock<IDbConnection>();
        connectionMock.Setup(connection => connection.CreateCommand()).Returns(commandMock.Object);

        var connectionFactoryMock = new Mock<IDbConnectionFactory>();
        connectionFactoryMock.Setup(f => f.CreateConnection()).Returns(connectionMock.Object);

        var studentService = new StudentService(connectionFactoryMock.Object);

        var student = studentService.Get(3);

        commandMock.Verify(x => x.ExecuteReader(), Times.Once);
        commandMock.VerifySet(c => c.CommandText = StoredProcedures.Students.Get, Times.Once);
        commandMock.VerifySet(c => c.CommandType = CommandType.StoredProcedure, Times.Once);

        Assert.NotNull(student);
        Assert.Equal(3, student.Id);
        Assert.Equal(2, student.HouseId);
        Assert.Equal("FirstName", student.FirstName);
        Assert.Equal("LastName", student.LastName);
    }


    [Fact]
    public void CreateStudent_Executes_Command_WithCorrectParams()
    {
        var parameterCollectionMock = new Mock<IDataParameterCollection>();

        var commandMock = new Mock<IDbCommand>();
        commandMock.Setup(command => command.Parameters).Returns(parameterCollectionMock.Object);
        commandMock.Setup(command => command.ExecuteNonQuery()).Returns(1);

        var connectionMock = new Mock<IDbConnection>();
        connectionMock.Setup(connection => connection.CreateCommand()).Returns(commandMock.Object);

        var connectionFactoryMock = new Mock<IDbConnectionFactory>();
        connectionFactoryMock.Setup(f => f.CreateConnection()).Returns(connectionMock.Object);

        var studentService = new StudentService(connectionFactoryMock.Object);

        studentService.Create(new Student(1, "First", "Last", 2));

        commandMock.Verify(x => x.ExecuteNonQuery(), Times.Once);
        commandMock.VerifySet(c => c.CommandText = StoredProcedures.Students.Create, Times.Once);
        commandMock.VerifySet(c => c.CommandType = CommandType.StoredProcedure, Times.Once);

        parameterCollectionMock.Verify(x => x.Add(It.IsAny<SqlParameter>()), Times.Exactly(3));

        parameterCollectionMock.Verify(x => x.Add(It.Is<SqlParameter>(p => p.ParameterName.Equals("@lastName") && p.SqlValue.ToString() == "Last")), Times.Once);
        parameterCollectionMock.Verify(x => x.Add(It.Is<SqlParameter>(p => p.ParameterName.Equals("@firstName") && p.SqlValue.ToString() == "First")), Times.Once);
        parameterCollectionMock.Verify(x => x.Add(It.Is<SqlParameter>(p => p.ParameterName.Equals("@houseId") && p.SqlValue.ToString() == "2")), Times.Once);
    }

    [Fact]
    public void UpdateStudent_Executes_Command_WithCorrectParams()
    {
        var parameterCollectionMock = new Mock<IDataParameterCollection>();

        var commandMock = new Mock<IDbCommand>();
        commandMock.Setup(command => command.Parameters).Returns(parameterCollectionMock.Object);
        commandMock.Setup(command => command.ExecuteNonQuery()).Returns(1);

        var connectionMock = new Mock<IDbConnection>();
        connectionMock.Setup(connection => connection.CreateCommand()).Returns(commandMock.Object);

        var connectionFactoryMock = new Mock<IDbConnectionFactory>();
        connectionFactoryMock.Setup(f => f.CreateConnection()).Returns(connectionMock.Object);

        var studentService = new StudentService(connectionFactoryMock.Object);

        studentService.Update(new Student(1, "First", "Last", 2));

        commandMock.Verify(x => x.ExecuteNonQuery(), Times.Once);
        commandMock.VerifySet(c => c.CommandText = StoredProcedures.Students.Update, Times.Once);
        commandMock.VerifySet(c => c.CommandType = CommandType.StoredProcedure, Times.Once);

        parameterCollectionMock.Verify(x => x.Add(It.IsAny<SqlParameter>()), Times.Exactly(4));

        parameterCollectionMock.Verify(x => x.Add(It.Is<SqlParameter>(p => p.ParameterName.Equals("@lastName") && p.SqlValue.ToString() == "Last")), Times.Once);
        parameterCollectionMock.Verify(x => x.Add(It.Is<SqlParameter>(p => p.ParameterName.Equals("@firstName") && p.SqlValue.ToString() == "First")), Times.Once);
        parameterCollectionMock.Verify(x => x.Add(It.Is<SqlParameter>(p => p.ParameterName.Equals("@houseId") && p.SqlValue.ToString() == "2")), Times.Once);
        parameterCollectionMock.Verify(x => x.Add(It.Is<SqlParameter>(p => p.ParameterName.Equals("@id") && p.SqlValue.ToString() == "1")), Times.Once);
    }

    [Fact]
    public void DeleteStudent_Executes_Command_WithCorrectParams()
    {
        var parameterCollectionMock = new Mock<IDataParameterCollection>();

        var commandMock = new Mock<IDbCommand>();
        commandMock.Setup(command => command.Parameters).Returns(parameterCollectionMock.Object);
        commandMock.Setup(command => command.ExecuteNonQuery()).Returns(1);

        var connectionMock = new Mock<IDbConnection>();
        connectionMock.Setup(connection => connection.CreateCommand()).Returns(commandMock.Object);

        var connectionFactoryMock = new Mock<IDbConnectionFactory>();
        connectionFactoryMock.Setup(f => f.CreateConnection()).Returns(connectionMock.Object);

        var studentService = new StudentService(connectionFactoryMock.Object);

        studentService.Delete(42);

        commandMock.Verify(x => x.ExecuteNonQuery(), Times.Once);
        commandMock.VerifySet(c => c.CommandText = StoredProcedures.Students.Delete, Times.Once);
        commandMock.VerifySet(c => c.CommandType = CommandType.StoredProcedure, Times.Once);

        parameterCollectionMock.Verify(x => x.Add(It.IsAny<SqlParameter>()), Times.Once);

        parameterCollectionMock.Verify(x => x.Add(It.Is<SqlParameter>(p => p.ParameterName.Equals("@id") && p.SqlValue.ToString() == "42")), Times.Once);
    }

    [Fact]
    public void FindByName_Returns_StudentList()
    {
        var parameterCollectionMock = new Mock<IDataParameterCollection>();

        var adapterMock = new Mock<IDbDataAdapter>();
        adapterMock.Setup(da => da.Fill(It.IsAny<DataSet>())).Callback((DataSet ds) => {
            ds.Tables.Clear();
            DataTable dt = ds.Tables.Add("Table");
            dt.Columns.Add("Id", typeof(int));
            dt.Columns.Add("HouseId", typeof(int));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Rows.Add(new object[] { 3, 2, "FirstName1", "LastName1" });
            dt.Rows.Add(new object[] { 4, 1, "FirstName2", "LastName2" });
        });

        var commandMock = new Mock<IDbCommand>();
        commandMock.Setup(c => c.Parameters).Returns(parameterCollectionMock.Object);

        var connectionMock = new Mock<IDbConnection>();
        connectionMock.Setup(connection => connection.CreateCommand()).Returns(commandMock.Object);

        var connectionFactoryMock = new Mock<IDbConnectionFactory>();
        connectionFactoryMock.Setup(f => f.CreateConnection()).Returns(connectionMock.Object);
        connectionFactoryMock.Setup(f => f.CreateDataAdapter()).Returns(adapterMock.Object);

        var studentService = new StudentService(connectionFactoryMock.Object);

        var students = studentService.FindByName("Name");

        Assert.NotNull(students);
        Assert.Equal(2, students.Count());

        var student1 = students.First();
        var student2 = students.Last();

        Assert.NotNull(student1);
        Assert.Equal(3, student1.Id);
        Assert.Equal(2, student1.HouseId);
        Assert.Equal("FirstName1", student1.FirstName);
        Assert.Equal("LastName1", student1.LastName);

        Assert.NotNull(student2);
        Assert.Equal(4, student2.Id);
        Assert.Equal(1, student2.HouseId);
        Assert.Equal("FirstName2", student2.FirstName);
        Assert.Equal("LastName2", student2.LastName);

        commandMock.VerifySet(c => c.CommandText = StoredProcedures.Students.FindByName, Times.Once);
        commandMock.VerifySet(c => c.CommandType = CommandType.StoredProcedure, Times.Once);
    }
}
