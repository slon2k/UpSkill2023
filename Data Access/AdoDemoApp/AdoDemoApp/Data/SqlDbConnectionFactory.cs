using AdoDemoApp.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace AdoDemoApp.Data;

public class SqlDbConnectionFactory : IDbConnectionFactory
{
    private readonly string connectionString;

    public SqlDbConnectionFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(connectionString);
    }

    public IDbDataAdapter CreateDataAdapter()
    {
        return new SqlDataAdapter();
    }
}
