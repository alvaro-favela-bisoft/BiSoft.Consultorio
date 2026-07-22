using BiSoft.Consultorio.Aplicacion.DTOs.Usuario;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Usuarios
{
    public static class RegistrarUsuario
    {
        private const string ENDPOINT_NAME = "Registrar Usuario";

        public static RouteGroupBuilder MapRegistrarUsuarioEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("",
                async (
                    [FromBody] RegistrarUsuarioRequest request,
                    UsuarioService usuarioService) =>
                {
                    var result = await usuarioService.RegistrarUsuario(request);
                    return Results.Created($"/api/usuarios/{result.Id}", result);
                })
                .Produces<RegistrarUsuarioResponse>(StatusCodes.Status201Created)
                .WithName(ENDPOINT_NAME)
                .WithSummary(ENDPOINT_NAME);

            return group;
        }
    }
}
