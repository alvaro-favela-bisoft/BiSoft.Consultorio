using BiSoft.Consultorio.Aplicacion.DTOs.Sala;
using BiSoft.Consultorio.Aplicacion.Services;

namespace Bisoft.Consultorio.Api.Endpoints.Salas
{
    public static class ConsultarSala
    {
        private const string ENDPOINT_NAME = "Obtener Sala";

        public static RouteGroupBuilder MapConsultarSalasEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("{id}",
                async (
                    Guid id,
                    SalaService salaService) =>
                {
                    var result = await salaService.ObtenerSala(id);
                    return Results.Ok(result);
                })
                .Produces<RegistrarSalaResponse>(StatusCodes.Status200OK)
                .WithName("Obtener Sala")
                .WithSummary(ENDPOINT_NAME);
            return group;
        }
    }
}