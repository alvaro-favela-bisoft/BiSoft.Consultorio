using Bisoft.Consultorio.Api.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class ActualizarCita
    {
        private const string ENDPOINT_NAME = "Actualizar Cita";

        public static RouteGroupBuilder MapActualizarCitasEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{id}",
                async (
                    Guid id,
                    [FromBody] RegistrarCitaRequest request,
                    CitaService citaService) =>
                {
                    var result = await citaService.ActualizarCita(id, request.Fecha, request.Motivo);
                    return Results.Ok(result);
                })
                .Produces<RegistrarCitaResponse>(StatusCodes.Status200OK)
                .WithName("Actualizar Cita")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}