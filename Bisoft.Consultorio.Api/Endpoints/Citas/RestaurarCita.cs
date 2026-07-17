using BiSoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.Services;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class RestaurarCita
    {
        private const string ENDPOINT_NAME = "Restaurar Cita";

        public static RouteGroupBuilder MapRestaurarCitasEndpoint(this RouteGroupBuilder group)
        {
            group.MapPatch("{id}/restore",
                async (Guid id, CitaService citaService) =>
                {
                    var result = await citaService.RestaurarCita(id);
                    return Results.Ok(result);
                })
                .Produces<RegistrarCitaResponse>(StatusCodes.Status200OK)
                .WithName("Restaurar Cita")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}
