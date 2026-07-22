using BiSoft.Consultorio.Aplicacion.DTOs.Usuario;
using BiSoft.Consultorio.Aplicacion.Services;

namespace Bisoft.Consultorio.Api.Endpoints.Usuarios
{
    public static class ConsultarUsuario
    {
        private const string ENDPOINT_NAME = "Consultar Usuario";

        public static RouteGroupBuilder MapConsultarUsuarioEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("{usuarioId}",
                async (
                    Guid usuarioId,
                    UsuarioService usuarioService) =>
                {
                    var result = await usuarioService.ConsultarUsuario(usuarioId);
                    return Results.Ok(result);
                })
                .Produces<ConsultarUsuarioResponse>(StatusCodes.Status200OK)
                .WithName(ENDPOINT_NAME)
                .WithSummary(ENDPOINT_NAME);

            return group;
        }
    }
}
