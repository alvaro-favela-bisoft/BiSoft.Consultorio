namespace Bisoft.Consultorio.Api.DTOs.Configurations
{
    public record GeneralConfigurations
    (
        string ConnectionString,
        string UsuariosConnectionString,
        int RateLimit,
        JwtConfigurations JWT
    );
}
