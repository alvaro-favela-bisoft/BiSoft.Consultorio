using Bisoft.Consultorio.Api.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class RegistrarPaciente
    {
        private const string ENDPOINT_NAME = "Registrar Paciente";

        public static RouteGroupBuilder MapRegistrarPacientesEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("",
                async (
                    [FromBody] RegistrarPacienteRequest request,
                    PacienteService pacienteService) =>
                {
                    var result = await pacienteService.RegistrarPaciente(request.Nombre, request.Condicion);
                    return Results.Created($"/api/pacientes/{result.Id}", result);
                })
                .Produces<RegistrarPacienteResponse>(StatusCodes.Status201Created)
                .WithName("Registrar Paciente")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}
