using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class EliminarPaciente
    {
        private const string ENDPOINT_NAME = "Eliminar Paciente";

        public static RouteGroupBuilder MapEliminarPacientesEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("{pacienteId}",
                async (
                    [FromRoute] Guid pacienteId,
                    PacienteService pacienteService,
                    CancellationToken ct
                    ) =>
                {
                    await pacienteService.EliminarPaciente(pacienteId);
                    return Results.Ok();
                }
                )
                .Produces(StatusCodes.Status200OK)
                .WithSummary(ENDPOINT_NAME)
                .WithName("Eliminar Paciente");
            return group;
        }
    }
}
