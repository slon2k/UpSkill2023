﻿using Npgsql;

namespace AdoDemoApp;

public class PgDbInitializer : IDbInitializer
{
    private readonly string connectionString;

    public PgDbInitializer(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void CreateTables()
    {
        string query =
            "CREATE TABLE Student(" +
            "Id INT GENERATED BY DEFAULT AS IDENTITY (START WITH 1 INCREMENT BY 1) PRIMARY KEY," +
            "FirstName VARCHAR(100)," +
            "LastName VARCHAR(100)" +
            ");";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            NpgsqlCommand command = new(query, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Table created");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
        };
    }
}
