using System.Data.SqlClient;
using AdoDemoApp.Interfaces;

namespace AdoDemoApp.Data;

public class DbInitializer : IDbInitializer
{
    private readonly string connectionString;

    public DbInitializer(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void CreateTables()
    {
        string query =
            "CREATE TABLE House(" +
            "Id INT IDENTITY(1,1) PRIMARY KEY," +
            "Name NVARCHAR(100)" +
            ");" +
            "CREATE TABLE Student(" +
            "Id INT IDENTITY(1,1) PRIMARY KEY," +
            "HouseId INT FOREIGN KEY REFERENCES House(Id)," +
            "FirstName NVARCHAR(100)," +
            "LastName NVARCHAR(100)" +
            ");";

        using (SqlConnection connection = new(connectionString))
        {
            SqlCommand command = new(query, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Tables created\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        };
    }

    public void DropTables()
    {
        string query =
            "DROP TABLE Student;" +
            "DROP TABLE House;";

        using (SqlConnection connection = new(connectionString))
        {
            SqlCommand command = new(query, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Tables deleted\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        };
    }

    public void SeedData()
    {
        SeedHouses();
        SeedStudents();
    }

    private void SeedStudents()
    {
        CreateStudent("Harry", "Potter", 1);
        CreateStudent("Hermione", "Granger", 1);
        CreateStudent("Ron", "Weasley", 1);
        CreateStudent("Cedric", "Diggory", 2);
        CreateStudent("Susan", "Bones", 2);
        CreateStudent("Luna", "Lovegood", 3);
        CreateStudent("Padma", "Patil", 3);
        CreateStudent("Draco", "Malfoy", 4);
        CreateStudent("Gregory", "Goyle", 4);
        CreateStudent("Vincent", "Crabbe", 4);
    }

    private void SeedHouses()
    {
        CreateHouse(1, "Gryffindor");
        CreateHouse(2, "Hufflepuff");
        CreateHouse(3, "Ravenclaw");
        CreateHouse(4, "Slytherin");
    }

    private void CreateHouse(int id, string name)
    {
        string query =
            "SET IDENTITY_INSERT House ON;" +
            "INSERT INTO House (Id, Name)" +
            $"VALUES (@id, @name);" +
            "SET IDENTITY_INSERT House OFF;";

        using (SqlConnection connection = new(connectionString))
        {
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("id", id);
            command.Parameters.AddWithValue("name", name);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("House created: " + name);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        };
    }

    private void CreateStudent(string firstName, string lastName, int houseId)
    {
        string query =
            "INSERT INTO Student (FirstName, LastName, HouseId)" +
            "VALUES (@firstName, @lastName, @houseId);";

        using (SqlConnection connection = new(connectionString))
        {
            SqlCommand command = new(query, connection);

            command.Parameters.AddWithValue("@houseId", houseId);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@lastName", lastName);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine($"Student created: {firstName} {lastName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        };
    }
}
