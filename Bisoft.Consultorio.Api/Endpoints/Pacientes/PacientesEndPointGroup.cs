namespace Bisoft.Consultorio.Api.Endpoints.Pacientes
{
    public static class PacientesEndPointGroup
    {
        public static RouteGroupBuilder MapPacientesEndpoints(this RouteGroupBuilder group)
        {
            var pacientesGroup = group.MapGroup("pacientes").WithTags("Pacientes");
            return pacientesGroup.MapEndpoints();
        }
        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapRegistrarPacientesEndpoint();
            group.MapsConsultarPacientesEndpoint();
            group.MapActualizarPacientesEndpoint();
            group.MapEliminarPacientesEndpoint();
            return group;
        }
    }
}
