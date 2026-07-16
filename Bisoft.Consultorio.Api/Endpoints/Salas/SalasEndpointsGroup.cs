namespace Bisoft.Consultorio.Api.Endpoints.Salas
{
    public static class SalasEndpointsGroup
    {
        public static RouteGroupBuilder MapSalasEndpoints(this RouteGroupBuilder group)
        {
            var salasGroup = group.MapGroup("salas").WithTags("Salas");
            return salasGroup.MapEndpoints();
        }

        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapRegistrarSalasEndpoint();
            group.MapConsultarSalasEndpoint();
            group.MapActualizarSalasEndpoint();
            group.MapEliminarSalasEndpoint();
            return group;
        }
    }
}