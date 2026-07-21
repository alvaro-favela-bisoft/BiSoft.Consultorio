
namespace Bisoft.Consultorio.Api.Endpoints.Security
{
    public static class SecurityEndpointGroup
    {
        public static RouteGroupBuilder MapSecurityEndpoints(this RouteGroupBuilder group)
        {
            var endpointGroup = group.MapGroup("auth").WithTags("Security");
            endpointGroup.MapEndpoints();
            return endpointGroup;
        }

        private static RouteGroupBuilder MapEndpoints(this RouteGroupBuilder group)
        {
            group.MapLogin();
            return group;
        }
    }
}
