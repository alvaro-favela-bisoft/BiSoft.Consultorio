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
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);
                var conncectionStrings = builder.Configuration["DatabaseConnections:Consultorio:ConnectionStrings"];

                // ========== REGISTRAR SERVICIOS (ANTES DE builder.Build()) ==========

                // Doctores
                builder.Services.AddScoped<DoctorService>();
                builder.Services.AddScoped<DoctorDomainService>();
                builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();

                // Pacientes
                builder.Services.AddScoped<PacienteService>();
                builder.Services.AddScoped<PacienteDomainService>();
                builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();

                // Salas
                builder.Services.AddScoped<SalaService>();
                builder.Services.AddScoped<SalaDomainService>();
                builder.Services.AddScoped<ISalaRepository, SalaRepository>();

                // Citas
                builder.Services.AddScoped<CitaService>();
                builder.Services.AddScoped<CitaDomainService>();
                builder.Services.AddScoped<ICitaRepository, CitaRepository>();

                // DbContext
                builder.Services.AddDbContext<ConsultorioContext>(
                    options => options.UseSqlite(conncectionStrings)
                );

                // Logging
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.SQLite(
                        sqliteDbPath: "Logs/Logs.db",
                        tableName: "Logs",
                        restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                    )
                    .CreateLogger();

                // API Documentation
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // CORS
                builder.Services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll",
                        builder =>
                        {
                            builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                        });
                });

                // Authorization
                builder.Services.AddAuthorization();
                builder.Services.AddOpenApi();

                // ========== CONSTRUIR LA APLICACIÓN ==========
                var app = builder.Build();

                // ========== CONFIGURAR PIPELINE (DESPUÉS DE builder.Build()) ==========

                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.UseCors("AllowAll");
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseMiddleware<ErrorHandlerMiddleware>();

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