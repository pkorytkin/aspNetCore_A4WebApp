using Microsoft.Data.SqlClient;

namespace A4WebApp.Interfaces
{
    public interface IDatabaseConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}
