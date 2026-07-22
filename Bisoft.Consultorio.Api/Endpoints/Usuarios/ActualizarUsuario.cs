using BiSoft.Consultorio.Aplicacion.DTOs.Usuario;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Usuarios
{
    public static class ActualizarUsuario
    {
        private const string ENDPOINT_NAME = "Actualizar Usuario";

        public static RouteGroupBuilder MapActualizarUsuarioEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{usuarioId}", 
                async (
                    Guid usuarioId,
                    [FromBody] ActualizarUsuarioRequest request,
                    UsuarioService usuarioService) =>
                {
                    var result = await usuarioService.ActualizarUsuario(usuarioId, request);
                    return Results.Ok(result);
                })
                .Produces<ActualizarUsuarioResponse>(StatusCodes.Status200OK)
                .WithName(ENDPOINT_NAME)
                .WithSummary(ENDPOINT_NAME);

            return group;
        }
    }
}
