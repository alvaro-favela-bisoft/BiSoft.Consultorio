using Bisoft.Consultorio.Api.DTOs.Configurations;
using Bisoft.Consultorio.Api.Helpers.HealthCheck;
using BiSoft.Consultorio.Aplicacion.Services;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Dominio.Services;
using BiSoft.Consultorio.Infraestructura.Contexts;
using BiSoft.Consultorio.Infraestructura.Repositories.Consultorio;
using BiSoft.Consultorio.Infraestructura.Repositories.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Globalization;
using System.Threading.RateLimiting;

namespace Bisoft.Consultorio.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection InyectarServicios(this IServiceCollection services)
        {
            // Doctor
            services.AddScoped<DoctorService>();
            services.AddScoped<DoctorDomainService>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();

            // Pacientes
            services.AddScoped<PacienteService>();
            services.AddScoped<PacienteDomainService>();
            services.AddScoped<IPacienteRepository, PacienteRepository>();

            // Salas
            services.AddScoped<SalaService>();
            services.AddScoped<SalaDomainService>();
            services.AddScoped<ISalaRepository, SalaRepository>();

            // Citas
            services.AddScoped<CitaService>();
            services.AddScoped<CitaDomainService>();
            services.AddScoped<ICitaRepository, CitaRepository>();

            // Usuarios
            services.AddScoped<UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
        public static IServiceCollection InyectarContextos(this IServiceCollection services, string consultorioConnectionString, string usuariosConnectionString)
        {
            services.AddDbContext<ConsultorioContext>(options => options.UseSqlite(consultorioConnectionString));

            services.AddDbContext<UsuariosContext>(options => options.UseSqlite(usuariosConnectionString));

            return services;
        }
        public static IServiceCollection ConfigurarSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        public static IServiceCollection ConfigurarCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Program.CORS_POLICY_NAME, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            return services;
        }
        public static IServiceCollection ConfugurarHealthChecks(this IServiceCollection services, string connectionString)
        {
            services.AddHealthChecks()
                .AddCheck("Liveness", () => HealthCheckResult.Healthy($"API iniciada correctamente"))
                .AddCheck("Database", new DatabaseHealthCheck(connectionString), tags: ["ready"]);
            return services;
        }
        public static IServiceCollection ConfigureRateLimiter(this IServiceCollection services, int allowedRequestPerMinute)
        {
            services.AddRateLimiter(config =>
            {
                config.OnRejected = (context, ct) =>
                {
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        context.HttpContext.Response.Headers.RetryAfter = retryAfter.TotalSeconds.ToString(NumberFormatInfo.InvariantInfo);
                    }
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    context.HttpContext.Response.WriteAsync("Demasiados requests. Intente mas tarde", cancellationToken: ct);
                    return new ValueTask();
                };
                config.AddFixedWindowLimiter(Program.RATE_LIMITER_POLICY_NAME, options =>
                {
                    options.PermitLimit = allowedRequestPerMinute;
                    options.Window = TimeSpan.FromMinutes(1);
                    options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                    options.QueueLimit = 0;
                });
            });
            return services;
        }
        public static IServiceCollection ConfigureLogger(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.SQLite(
                    sqliteDbPath: "Logs/Logs.db",
                    tableName: "Logs",
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                )
                .WriteTo.Console()
                .CreateLogger();
            services.AddSerilog();
            return services;
        }
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection service, JwtConfigurations jwtConfig)
        {
            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtConfig.Issuer,
                        ValidAudience = jwtConfig.Audience,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),

                        ClockSkew = TimeSpan.Zero
                    };
                });

            return service;
        }
    }
}