
namespace Bisoft.Consultorio.Api.DTOs.Configurations
{
    public record JwtConfigurations
    (
        string Issuer,
        string Audience,
        string SecretKey,
        int ExpirationMinutes
    );
}
