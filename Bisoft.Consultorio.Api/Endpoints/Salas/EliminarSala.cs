using BiSoft.Consultorio.Aplicacion.Services;

namespace Bisoft.Consultorio.Api.Endpoints.Salas
{
    public static class EliminarSala
    {
        private const string ENDPOINT_NAME = "Eliminar Sala";

        public static RouteGroupBuilder MapEliminarSalasEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("{id}",
                async (
                    Guid id,
                    SalaService salaService) =>
                {
                    await salaService.EliminarSala(id);
                    return Results.NoContent();
                })
                .Produces(StatusCodes.Status204NoContent)
                .WithName("Eliminar Sala")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}