using Bisoft.Consultorio.Api.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Doctores
{
    public static class ActualizarDoctor
    {
        private const string ENDPOINT_NAME = "Actualizar Doctor";
        public static RouteGroupBuilder MapActualizarDoctoresEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{doctorId}", 
                async (
                    [FromRoute] Guid doctorId,
                    [FromBody] ActualizarDoctorRequest request, 
                    DoctorService doctorService
                    ) =>
            {
                var updated = await doctorService.ActualizarDoctor(doctorId, request.Nombre, request.Especialidad);
                return Results.Ok(updated);
            })
                .Produces<ActualizarDoctorResponse>(StatusCodes.Status200OK)
                .WithName("Actualizar Doctor")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}
