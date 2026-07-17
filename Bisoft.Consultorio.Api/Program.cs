using Bisoft.Consultorio.Api.DTOs.Paciente;
using Bisoft.Consultorio.Api.Extensions;
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
                var connectionString = builder.Configuration["DatabaseConnections:Consultorio:ConnectionStrings"];
                if (string.IsNullOrWhiteSpace(connectionString))

                {

                    throw new InvalidOperationException("SQLite connection string is not configured. Check appsettings.json for DatabaseConnections:Consultorio:ConnectionString.");

                }
                builder.Services.InyectarServicios()
                       .InyectarContextos(connectionString)
                       .ConfigurarSwagger()
                       .ConfigurarCors()
                       .ConfugurarHealthChecks(connectionString);

                // ====================


                // DbContext
                builder.Services.AddDbContext<ConsultorioContext>(
                    options => options.UseSqlite(connectionString)
                );

                // Logging
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.SQLite(
                        sqliteDbPath: "Logs/Logs.db",
                        tableName: "Logs",
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                    )
                    .CreateLogger();

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

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.UseCors("AllowAll");

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