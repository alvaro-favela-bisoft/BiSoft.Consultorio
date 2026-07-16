namespace Bisoft.Consultorio.Api.Endpoints.Doctores
{
    public static class DoctoresEndPoinstGroup 
    {
        public static RouteGroupBuilder MapDoctoresEndpoints(this RouteGroupBuilder group) 
        {
            var doctoresGroup = group.MapGroup("doctores").WithTags("Doctores");
            return doctoresGroup.MapEndpoints();
        }
        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapConsultarDoctoresEndpoint();
            group.MapRegistrarDoctoresEndpoint();
            group.MapActualizarDoctoresEndpoint();
            group.MapEliminarDoctoresEndpoint();
            return group;
        }
    }
}
