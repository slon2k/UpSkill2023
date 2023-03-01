using System.Data;

namespace AdoDemoApp.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();

    IDbDataAdapter CreateDataAdapter();

    IDbInitializer CreateDbInitializer();

    IDbDataParameter CreateDataParameter(string parameterName, object value);
}
