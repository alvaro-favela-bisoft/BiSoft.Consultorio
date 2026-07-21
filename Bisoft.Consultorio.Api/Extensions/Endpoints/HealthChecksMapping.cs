using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.RateLimiting;

namespace Bisoft.Consultorio.Api.Extensions.Endpoints
{
    public static class HealthChecksMapping
    {
        public static WebApplication AddHealthChecks(this WebApplication app, string IRateLimiterPolicy)
        {
            app.MapHealthChecks("/health-check").AllowAnonymous().RequireRateLimiting(IRateLimiterPolicy);
            app.AddHealthDetails().RequireRateLimiting(IRateLimiterPolicy);
            app.AddLiveness().RequireRateLimiting(IRateLimiterPolicy);
            app.AddReadiness().RequireRateLimiting(IRateLimiterPolicy);
            return app;
        }
        private static IEndpointConventionBuilder AddHealthDetails(this WebApplication app)
        {
            return app.MapHealthChecks("/health-details", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }).AllowAnonymous();
        }
        public static IEndpointConventionBuilder AddLiveness(this WebApplication app)
        {
            return app.MapHealthChecks("/health/live", new HealthCheckOptions
            {
                Predicate = (check) => check.Name == "Liveness"
            });
        }
        public static IEndpointConventionBuilder AddReadiness(this WebApplication app)
        {
            return app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = (check) => check.Name == "ready"
            });
        }
    }
}
