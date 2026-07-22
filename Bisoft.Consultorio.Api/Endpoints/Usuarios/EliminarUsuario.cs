using BiSoft.Consultorio.Aplicacion.Services;

namespace Bisoft.Consultorio.Api.Endpoints.Usuarios
{
    public static class EliminarUsuario
    {
        private const string ENDPOINT_NAME = "Eliminar Usuario";

        public static RouteGroupBuilder MapEliminarUsuarioEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("{usuarioId}",
                async (
                    Guid usuarioId,
                    UsuarioService usuarioService) =>
                {
                    await usuarioService.EliminarUsuario(usuarioId);
                    return Results.NoContent();
                })
                .Produces(StatusCodes.Status204NoContent)
                .WithName(ENDPOINT_NAME)
                .WithSummary(ENDPOINT_NAME);

            return group;
        }
    }
}
