namespace Bisoft.Consultorio.Api.Endpoints.Usuarios
{
    public static class UsuariosEndpointGroup
    {
        public static RouteGroupBuilder MapUsuariosEndpoints(this RouteGroupBuilder group)
        {
            var usuariosGroup = group.MapGroup("usuarios").WithTags("Usuarios");
            return usuariosGroup.MapEndpoints();
        }

        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapConsultarUsuarioEndpoint();
            group.MapRegistrarUsuarioEndpoint();
            group.MapActualizarUsuarioEndpoint();
            group.MapEliminarUsuarioEndpoint();
            return group;
        }
    }
}
