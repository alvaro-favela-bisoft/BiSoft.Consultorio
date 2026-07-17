// csharp Bisoft.Consultorio.Api\Helpers\HealthCheck\DatabaseHealthCheck.cs
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Bisoft.Consultorio.Api.Helpers.HealthCheck
{
    public class DatabaseHealthCheck : IHealthCheck
    {
        private readonly string _connectionString;

        public DatabaseHealthCheck(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var canConnect = CanConnectToDatabase(_connectionString);

            if (!canConnect)
            {
                return Task.FromResult(new HealthCheckResult(
                    context.Registration.FailureStatus,
                    description: "Database connection is unhealthy"));
            }

            return Task.FromResult(new HealthCheckResult(
                HealthStatus.Healthy,
                description: "Database connection is healthy"));
        }

        private bool CanConnectToDatabase(string connectionString)
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);
                connection.Open();
                var canConnect = connection.State == System.Data.ConnectionState.Open;
                connection.Dispose();
                return canConnect;
            }
            catch
            {
                return false;
            }
        }
    }
}