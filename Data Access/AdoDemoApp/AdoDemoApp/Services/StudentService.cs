using AdoDemoApp.Interfaces;
using AdoDemoApp.Models;
using System.Data;
using System.Data.SqlClient;
using static AdoDemoApp.StoredProcedures;

namespace AdoDemoApp.Services;

public class StudentService : IStudentService
{
    private readonly IDbConnectionFactory connectionFactory;

    public StudentService(IDbConnectionFactory factory)
    {
        connectionFactory = factory;
    }

    public void Create(Student student)
    {
        using IDbConnection connection = connectionFactory.CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        
        command.CommandText = Students.Create;
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(new SqlParameter("@firstName", student.FirstName));
        command.Parameters.Add(new SqlParameter("@lastName", student.LastName));
        command.Parameters.Add(new SqlParameter("@houseId", student.HouseId));

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
        using IDbConnection connection = connectionFactory.CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        
        command.CommandText = Students.Delete;
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(new SqlParameter("@id", id));

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
        using IDbConnection connection = connectionFactory.CreateConnection();
        using IDbCommand command = connection.CreateCommand();

        command.CommandType = CommandType.StoredProcedure;
        command.CommandText = Students.FindByName;

        command.Parameters.Add(new SqlParameter("@name", name));

        IDbDataAdapter dataAdapter = connectionFactory.CreateDataAdapter();
        dataAdapter.SelectCommand = command;

        try
        {
            connection.Open();

            var ds = new DataSet();

            dataAdapter.Fill(ds);

            var dt = ds.Tables[0];

            return
                from row in dt.AsEnumerable()
                select new Student(
                    row.Field<int>("Id"),
                    row.Field<string>("FirstName") ?? "",
                    row.Field<string>("LastName") ?? "",
                    row.Field<int>("HouseId"));
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.Message);
            throw;
        }
    }

    public Student? Get(int id)
    {
        using IDbConnection connection = connectionFactory.CreateConnection();
        using IDbCommand command = connection.CreateCommand();

        command.CommandText = Students.Get;
        command.CommandType = CommandType.StoredProcedure;

        try
        {
            connection.Open();

            var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Student(
                    reader.GetInt32(0),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetInt32(1)
                );
            }

            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public IEnumerable<Student> GetAll()
    {
        using IDbConnection connection = connectionFactory.CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        
        command.CommandText = Students.GetAll;
        command.CommandType = CommandType.StoredProcedure;

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
        using IDbConnection connection = connectionFactory.CreateConnection();
        using IDbCommand command = connection.CreateCommand();
        
        command.CommandText = Students.Update;
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.Add(new SqlParameter("@id", student.Id));
        command.Parameters.Add(new SqlParameter("@firstName", student.FirstName));
        command.Parameters.Add(new SqlParameter("@lastName", student.LastName));
        command.Parameters.Add(new SqlParameter("@houseId", student.HouseId));

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
