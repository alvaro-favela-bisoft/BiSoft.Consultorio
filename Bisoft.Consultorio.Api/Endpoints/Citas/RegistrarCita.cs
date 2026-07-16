using Bisoft.Consultorio.Api.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class RegistrarCita
    {
        private const string ENDPOINT_NAME = "Registrar Cita";

        public static RouteGroupBuilder MapRegistrarCitasEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("",
                async (
                    [FromBody] RegistrarCitaRequest request,
                    CitaService citaService) =>
                {
                    var result = await citaService.RegistrarCita(
                        request.Fecha,
                        request.PacienteId,
                        request.DoctorId,
                        request.SalaId,
                        request.Motivo);
                    return Results.Created($"/api/citas/{result.Id}", result);
                })
                .Produces<RegistrarCitaResponse>(StatusCodes.Status201Created)
                .WithName("Registrar Cita")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}