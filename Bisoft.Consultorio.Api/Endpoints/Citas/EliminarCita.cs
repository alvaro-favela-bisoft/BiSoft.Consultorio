using BiSoft.Consultorio.Aplicacion.Services;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class EliminarCita
    {
        private const string ENDPOINT_NAME = "Eliminar Cita";

        public static RouteGroupBuilder MapEliminarCitasEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("{id}",
                async (
                    Guid id,
                    CitaService citaService) =>
                {
                    await citaService.EliminarCita(id);
                    return Results.NoContent();
                })
                .Produces(StatusCodes.Status204NoContent)
                .WithName("Eliminar Cita")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}