using AdoDemoApp.Interfaces;
using AdoDemoApp.Models;
using System.Data.SqlClient;
using static AdoDemoApp.StoredProcedures;

namespace AdoDemoApp.Services;

public class StudentService : IStudentService
{
    private string connectionString;

    public StudentService(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void Create(Student student)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        using SqlCommand command = connection.CreateCommand();
        command.CommandText = StoredProcedures.Students.Create;
        command.CommandType = System.Data.CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@firstName", student.FirstName);
        command.Parameters.AddWithValue("@lastName", student.LastName);
        command.Parameters.AddWithValue("@houseId", student.HouseId);

        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public void Delete(int id)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        using SqlCommand command = connection.CreateCommand();
        command.CommandText = StoredProcedures.Students.Delete;
        command.CommandType = System.Data.CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@id", id);

        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public IEnumerable<Student> FindByName(string name)
    {
        throw new NotImplementedException();
    }

    public Student? Get(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Student> GetAll()
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        using SqlCommand command = connection.CreateCommand();
        command.CommandText = StoredProcedures.Students.GetAll;
        command.CommandType = System.Data.CommandType.StoredProcedure;

        try
        {
            var students = new List<Student>();

            connection.Open();

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                students.Add(new Student(
                    reader.GetInt32(0),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetInt32(1)
                )); ;
            }

            return students;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public void Update(Student student)
    {
        using SqlConnection connection = new SqlConnection(connectionString);
        using SqlCommand command = connection.CreateCommand();
        command.CommandText = StoredProcedures.Students.Update;
        command.CommandType = System.Data.CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@id", student.Id);
        command.Parameters.AddWithValue("@firstName", student.FirstName);
        command.Parameters.AddWithValue("@lastName", student.LastName);
        command.Parameters.AddWithValue("@houseId", student.HouseId);

        try
        {
            connection.Open();
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
