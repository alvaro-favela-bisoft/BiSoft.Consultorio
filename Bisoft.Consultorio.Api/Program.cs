using Bisoft.Consultorio.Api.DTOs.Paciente;
using Bisoft.Consultorio.Api.Extensions;
using Bisoft.Consultorio.Api.Extensions.Endpoints;
using Bisoft.Consultorio.Api.Middlewares;
using BiSoft.Consultorio.Aplicacion.Services;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Dominio.Services;
using BiSoft.Consultorio.Infraestructura.Contexts;
using BiSoft.Consultorio.Infraestructura.Repositories.Consultorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Bisoft.Consultorio.Api
{
    public static class Program
    {
        public const string RATE_LIMITER_POLICY_NAME = "fixed"; 
        public const string CORS_POLICY_NAME = "AllowAll";
    
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                var configuration = builder.Configuration.GetGeneralConfigurations();

                builder.Services.AddSingleton(configuration.JWT);
                builder.Services.InyectarServicios()
                       .InyectarContextos(configuration.ConnectionString, configuration.UsuariosConnectionString)
                       .ConfigurarSwagger()
                       .ConfigurarCors()
                       .ConfugurarHealthChecks(configuration.ConnectionString)
                       .ConfigureRateLimiter(configuration.RateLimit)
                       .ConfigureLogger()
                       .ConfigureAuthentication(configuration.JWT);

                // ====================


                // DbContext
                /*builder.Services.AddDbContext<ConsultorioContext>(
                    options => options.UseSqlite(configuration)
                );*/

                // Logging
                

                // API Documentation
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // Authorization
                builder.Services.AddAuthorization();
                builder.Services.AddOpenApi();

                // CONSTRUIR LA APLICACIÓN
                var app = builder.Build();

                // CONFIGURAR PIPELINE
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }

                app.UseCors("AllowAll");
                app.UseHttpsRedirection();
                app.UseAuthentication();
                app.UseAuthorization();

                // OpenAPI
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseMiddleware<ErrorHandlerMiddleware>();

                // CORS
                app.UseCors(CORS_POLICY_NAME);

                app.AddHealthChecks(Program.RATE_LIMITER_POLICY_NAME);
                // Mapear endpoints
                app.MapEndpoints();

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al iniciar la aplicación {ex.Message}");
                Console.ReadKey();
                Environment.Exit(1);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}