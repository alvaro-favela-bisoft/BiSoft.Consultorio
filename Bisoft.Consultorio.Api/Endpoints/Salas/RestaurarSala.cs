using BiSoft.Consultorio.Aplicacion.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.Services;

namespace Bisoft.Consultorio.Api.Endpoints.Salas
{
    public static class RestaurarSala
    {
        private const string ENDPOINT_NAME = "Restaurar Sala";

        public static RouteGroupBuilder MapRestaurarSalasEndpoint(this RouteGroupBuilder group)
        {
            group.MapPatch("{id}/restore",
                async (Guid id, SalaService salaService) =>
                {
                    var result = await salaService.RestaurarSala(id);
                    return Results.Ok(result);
                })
                .Produces<RegistrarSalaResponse>(StatusCodes.Status200OK)
                .WithName("Restaurar Sala")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}
