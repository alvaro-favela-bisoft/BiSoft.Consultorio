using BiSoft.Consultorio.Aplicacion.DTOs.Paciente;
using BiSoft.Consultorio.Aplicacion.Services;

namespace Bisoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class RestaurarPaciente
    {
        private const string ENDPOINT_NAME = "Restaurar Paciente";

        public static RouteGroupBuilder MapRestaurarPacientesEndpoint(this RouteGroupBuilder group)
        {
            group.MapPatch("{id}/restore",
                async (Guid id, PacienteService pacienteService) =>
                {
                    var result = await pacienteService.RestaurarPaciente(id);
                    return Results.Ok(result);
                })
                .Produces<RegistrarPacienteResponse>(StatusCodes.Status200OK)
                .WithName("Restaurar Paciente")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}
