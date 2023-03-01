using System.Data;

namespace AdoDemoApp.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();

    IDbDataAdapter CreateDataAdapter();
}
