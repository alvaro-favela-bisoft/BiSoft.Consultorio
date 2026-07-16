using BiSoft.Consultorio.Aplicacion.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class ConsultarPaciente
    { 
        private const string ENDPOINT_NAME = "Consultar Paciente";

        public static RouteGroupBuilder MapsConsultarPacientesEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("{pacienteId}",
                    async (
                        [FromRoute] Guid pacienteId,
                        PacienteService pacienteService,
                        CancellationToken ct
                        ) =>
                    {
                        var paciente = await pacienteService.ConsultarPaciente(pacienteId);
                        return Results.Ok(paciente);
                    }
                    )
                    .Produces<ConsultarPacienteResponse>(StatusCodes.Status200OK)
                    .WithSummary(ENDPOINT_NAME)
                    .WithName("Consultar Paciente");
            return group;
        }
    }
}
