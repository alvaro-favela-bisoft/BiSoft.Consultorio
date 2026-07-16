using Bisoft.Consultorio.Api.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Salas
{
    public static class RegistrarSala
    {
        private const string ENDPOINT_NAME = "Registrar Sala";

        public static RouteGroupBuilder MapRegistrarSalasEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("",
                async (
                    [FromBody] RegistrarSalaRequest request,
                    SalaService salaService) =>
                {
                    var result = await salaService.RegistrarSala(request.Nombre);
                    return Results.Created($"/api/salas/{result.Id}", result);
                })
                .Produces<RegistrarSalaResponse>(StatusCodes.Status201Created)
                .WithName("Registrar Sala")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}