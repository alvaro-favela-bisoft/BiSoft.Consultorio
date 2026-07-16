using BiSoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.Services;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class ConsultarCita
    {
        private const string ENDPOINT_NAME = "Obtener Cita";

        public static RouteGroupBuilder MapConsultarCitasEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("{id}",
                async (
                    Guid id,
                    CitaService citaService) =>
                {
                    var result = await citaService.ObtenerCita(id);
                    return Results.Ok(result);
                })
                .Produces<RegistrarCitaResponse>(StatusCodes.Status200OK)
                .WithName("Obtener Cita")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}