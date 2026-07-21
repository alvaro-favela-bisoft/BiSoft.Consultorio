using Bisoft.Consultorio.Api.DTOs.Configurations;
using Bisoft.Consultorio.Api.DTOs.Security;
using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Bisoft.Consultorio.Api.Endpoints.Security
{
    public static class Login
    {
        private const string ENDPOINT_NAME = "Login";
        private const string USUARIO = "administrador";
        private const string PASSWORD = "SuperSecreta123";
        public static RouteGroupBuilder MapLogin(this RouteGroupBuilder group)
        {
            group.MapGet("login", [AllowAnonymous]
                async (
                    JwtConfigurations jwtConfiguration,
                    [FromBody] LoginRequest request,
                    CancellationToken ct
                    ) =>
                    {
                        if (request.Usuario != USUARIO || request.Password != PASSWORD)
                            return Results.Unauthorized();

                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey));
                        var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                        var tokenOptions = new JwtSecurityToken(
                            issuer: jwtConfiguration.Issuer,
                            audience: jwtConfiguration.Audience,
                            expires: DateTime.Now.AddMinutes(15),
                            signingCredentials: credential,
                            claims: new List<Claim>()
                            );

                        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                        return Results.Ok(new LoginResponse {Token = token});
                    }
            )
            .Produces<ConsultarDoctorResponse>(StatusCodes.Status200OK)
            .WithDescription("Permite iniciar sesion.")
            .WithSummary(ENDPOINT_NAME)
            .WithName(ENDPOINT_NAME);
            return group;
        }
    }
}
