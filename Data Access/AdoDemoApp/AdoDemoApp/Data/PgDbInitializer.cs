﻿using AdoDemoApp.Interfaces;
using Npgsql;

namespace AdoDemoApp.Data;

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
            "CREATE TABLE House(" +
            "Id INT GENERATED BY DEFAULT AS IDENTITY (START WITH 1 INCREMENT BY 1) PRIMARY KEY," +
            "Name VARCHAR(100)" +
            ");" +
            "CREATE TABLE Student("+
            "Id INT GENERATED BY DEFAULT AS IDENTITY (START WITH 1 INCREMENT BY 1) PRIMARY KEY," +
            "HouseId INT, FOREIGN KEY(HouseId) REFERENCES House(Id),"+
            "FirstName VARCHAR(100)," +
            "LastName VARCHAR(100));";

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

    public void DropTables()
    {
        string query =
            "DROP TABLE IF EXISTS Student;" +
            "DROP TABLE IF EXISTS House;";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            NpgsqlCommand command = new(query, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Table dropped");
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
            "INSERT INTO House (Id, Name)" +
            "VALUES (@id, @name);";

        using (NpgsqlConnection connection = new(connectionString))
        {
            NpgsqlCommand command = new(query, connection);

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

        using (NpgsqlConnection connection = new(connectionString))
        {
            NpgsqlCommand command = new(query, connection);

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
