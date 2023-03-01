using AdoDemoApp.Interfaces;
using Npgsql;
using System.Data;
using System.Data.SqlClient;

namespace AdoDemoApp.Data;

public class PgDbConnectionFactory : IDbConnectionFactory
{
    private readonly string connectionString;

    public PgDbConnectionFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(connectionString);
    }

    public IDbDataAdapter CreateDataAdapter()
    {
        return new NpgsqlDataAdapter();
    }

    public IDbInitializer CreateDbInitializer()
    {
        return new PgDbInitializer(connectionString);
    }

    public IDbDataParameter CreateDataParameter(string parameterName, object value)
    {
        return new NpgsqlParameter(parameterName, value);
    }
}
