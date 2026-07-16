using Bisoft.Consultorio.Api.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Bisoft.Consultorio.Api.Endpoints.Doctores
{
    public static class RegistrarDoctor
    {
        private const string ENDPOINT_NAME = "Registrar Doctor";
        public static RouteGroupBuilder MapRegistrarDoctoresEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("",
                async (
                    [FromBody] RegistrarDoctorRequest request,
                    DoctorService doctorService) =>
            {
                var result = await doctorService.RegistrarDoctor(request.Nombre, request.Especialidad);
                return Results.Created($"/api/doctores/{result.Id}", result);
            })
                .Produces<RegistrarDoctorResponse>(StatusCodes.Status201Created)
                .WithName("Registrar Doctor")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}
