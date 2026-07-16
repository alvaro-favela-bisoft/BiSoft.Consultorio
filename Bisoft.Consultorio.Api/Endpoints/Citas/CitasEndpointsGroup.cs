namespace Bisoft.Consultorio.Api.Endpoints.Citas
{
    public static class CitasEndpointsGroup
    {
        public static RouteGroupBuilder MapCitasEndpoints(this RouteGroupBuilder group)
        {
            var citasGroup = group.MapGroup("citas").WithTags("Citas");
            return citasGroup.MapEndpoints();
        }

        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapRegistrarCitasEndpoint();
            group.MapConsultarCitasEndpoint();
            group.MapActualizarCitasEndpoint();
            group.MapEliminarCitasEndpoint();
            return group;
        }
    }
}