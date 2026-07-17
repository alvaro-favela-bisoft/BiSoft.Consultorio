using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.Services;

namespace Bisoft.Consultorio.Api.Endpoints.Doctores
{
    public static class RestaurarDoctor
    {
        private const string ENDPOINT_NAME = "Restaurar Doctor";

        public static RouteGroupBuilder MapRestaurarDoctoresEndpoint(this RouteGroupBuilder group)
        {
            group.MapPatch("{id}/restore",
                async (
                    Guid id,
                    DoctorService doctorService) =>
                {
                    var result = await doctorService.RestaurarDoctor(id);
                    return Results.Ok(result);
                })
                .Produces<ConsultarDoctorResponse>(StatusCodes.Status200OK)
                .WithName("Restaurar Doctor")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}
