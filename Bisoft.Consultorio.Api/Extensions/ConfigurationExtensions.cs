using Bisoft.Consultorio.Api.DTOs.Configurations;

namespace Bisoft.Consultorio.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static GeneralConfigurations GetGeneralConfigurations(this IConfiguration configuration) 
        {
            var connectionString = configuration["DatabaseConnections:Consultorio:ConnectionString"];
            var rateLimit = configuration.GetValue<int>("RateLimiting");
            var jwtConfig = configuration.GetJwtConfiguration();
            return new GeneralConfigurations(connectionString, rateLimit, jwtConfig);
        }
        private static string GetConnectionString(this IConfiguration configuration, string connectionName)
        {
            return configuration[$"DatabaseConnections:{connectionName}:ConnectionString"];
        }
        private static JwtConfigurations GetJwtConfiguration(this IConfiguration configuration)
        {
            var secretKey = configuration["Jwt:SecretKey"];
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var authConfig = new JwtConfigurations(secretKey, issuer, audience);
            return authConfig;
        }
    }
}
