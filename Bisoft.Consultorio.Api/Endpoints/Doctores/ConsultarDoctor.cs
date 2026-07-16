using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Doctores
{
    public static class ConsultarDoctor
    {
        private const string ENDPOINT_NAME = "Consultar Doctor";
        public static RouteGroupBuilder MapConsultarDoctoresEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("{doctorId}",
                    async (
                        [FromRoute] Guid doctorId,
                        DoctorService doctorService,
                        CancellationToken ct
                        ) =>
                    {
                        var doctor = await doctorService.ConsultarDoctor(doctorId);
                        return Results.Ok(doctor);
                    }
                    )
                    .Produces<ConsultarDoctorResponse>(StatusCodes.Status200OK)
                    .WithSummary(ENDPOINT_NAME)
                    .WithName("Consultar Doctor");
            return group;
        }
    }
}
