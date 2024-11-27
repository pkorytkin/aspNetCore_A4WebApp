using A4WebApp.Interfaces;
using Microsoft.Data.SqlClient;

namespace A4WebApp.Service
{
    public static class DatabaseExtensions
    {
        public static void AddDatabaseConnection(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseConnectionFactory, DatabaseConnectionFactory>();

        }
    }
    public class DatabaseConnectionFactory:IDatabaseConnectionFactory
    {
        private readonly string? _connectionString;

        public DatabaseConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MSSQL");
        }
        public SqlConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}
