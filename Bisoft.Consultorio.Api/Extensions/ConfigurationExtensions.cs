using Bisoft.Consultorio.Api.DTOs.Configurations;

namespace Bisoft.Consultorio.Api.Extensions
{
    public static class ConfigurationExtensions
    {
        public static GeneralConfigurations GetGeneralConfigurations(this IConfiguration configuration) 
        {
            var consultorioConnectionString = configuration["DatabaseConnections:Consultorio:ConnectionString"];
            var usuariosConnectionString = configuration["DatabaseConnections:Usuarios:ConnectionString"];
            var rateLimit = configuration.GetValue<int>("RateLimiting");
            var jwtConfig = configuration.GetJwtConfiguration();
            return new GeneralConfigurations(consultorioConnectionString, usuariosConnectionString, rateLimit, jwtConfig);
        }
        private static string GetConnectionString(this IConfiguration configuration, string connectionName)
        {
            return configuration[$"DatabaseConnections:{connectionName}:ConnectionString"];
        }
        private static JwtConfigurations GetJwtConfiguration(this IConfiguration configuration)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var secretKey = configuration["Jwt:SecretKey"];
            var expirationMinutes = configuration.GetValue<int>("Jwt:ExpirationMinutes");
            var authConfig = new JwtConfigurations(issuer, audience, secretKey, expirationMinutes);
            return authConfig;
        }
    }
}
