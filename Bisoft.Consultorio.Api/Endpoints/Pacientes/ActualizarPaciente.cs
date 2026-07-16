using Bisoft.Consultorio.Api.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class ActualizarPaciente
    {
        private const string ENDPOINT_NAME = "Actualizar Paciente";

        public static RouteGroupBuilder MapActualizarPacientesEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{pacienteId}",
                async (
                    [FromRoute] Guid pacienteId,
                    [FromBody] ActualizarPacienteRequest request,
                    PacienteService pacienteService) =>
                {
                    var updated = await pacienteService.ActualizarPaciente(pacienteId, request.Nombre, request.Condicion);
                    return Results.Ok(updated);
                })
                .Produces<ActualizarPacienteResponse>(StatusCodes.Status200OK)
                .WithName("Actualizar Paciente")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}
