﻿using AdoDemoApp;
using System.Data.SqlClient;
namespace DemoConsoleApp;

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
            "CREATE TABLE Student(" +
            "Id INT IDENTITY(1,1) PRIMARY KEY," +
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
                Console.WriteLine("Table created");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: "+ ex.Message);
            }
        }; 
    }
}