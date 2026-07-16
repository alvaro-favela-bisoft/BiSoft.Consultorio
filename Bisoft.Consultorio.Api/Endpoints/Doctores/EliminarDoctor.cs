using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Doctores
{
    public static class EliminarDoctor
    {
        private const string ENDPOINT_NAME = "Eliminar Doctor";

        public static RouteGroupBuilder MapEliminarDoctoresEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("{doctorId}",
                async (
                    [FromRoute] Guid doctorId,
                    DoctorService doctorService,
                    CancellationToken ct
                    ) =>
                {
                    await doctorService.EliminarDoctor(doctorId);
                    return Results.Ok();
                }
                )
                .Produces(StatusCodes.Status200OK)
                .WithSummary(ENDPOINT_NAME)
                .WithName("Eliminar Doctor");
            return group;
        }
    }
}
