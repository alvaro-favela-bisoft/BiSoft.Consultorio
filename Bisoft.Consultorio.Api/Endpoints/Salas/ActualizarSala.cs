using Bisoft.Consultorio.Api.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bisoft.Consultorio.Api.Endpoints.Salas
{
    public static class ActualizarSala
    {
        private const string ENDPOINT_NAME = "Actualizar Sala";

        public static RouteGroupBuilder MapActualizarSalasEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("{id}",
                async (
                    Guid id,
                    [FromBody] RegistrarSalaRequest request,
                    SalaService salaService) =>
                {
                    var result = await salaService.ActualizarSala(id, request.Nombre);
                    return Results.Ok(result);
                })
                .Produces<RegistrarSalaResponse>(StatusCodes.Status200OK)
                .WithName("Actualizar Sala")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}