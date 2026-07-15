
using Bisoft.Consultorio.Api.DTOs;
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
            try {
                var builder = WebApplication.CreateBuilder(args);
                var conncectionStrings = builder.Configuration["DatabaseConnections:Consultorio:ConnectionStrings"];

                builder.Services.AddScoped<DoctorService>();
                builder.Services.AddScoped<DoctorDomainService>();
                builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
                builder.Services.AddScoped<PacienteService>();
                builder.Services.AddScoped<PacienteDomainService>();
                builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
                builder.Services.AddDbContext<ConsultorioContext>(
                        options => options.UseSqlite(conncectionStrings)
                    );
                Log.Logger = new LoggerConfiguration()
                            .WriteTo.SQLite(
                                sqliteDbPath: "Logs/Logs.db",
                                tableName: "Logs",
                                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information
                            )
                            .CreateLogger();
                //builder.Services.AddSerilog();

                // Add services to the container.
                builder.Services.AddAuthorization();

                // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
                builder.Services.AddOpenApi();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                }

                app.UseHttpsRedirection();

                app.UseAuthorization();

                app.MapGet("api/doctores/{doctorId}",
                    async (
                        [FromRoute] Guid doctorId,
                        DoctorService doctorService,
                        CancellationToken ct
                        ) =>
                    {
                        try
                        {
                            var doctor = await doctorService.ConsultarDoctor(doctorId);
                            return Results.Ok(doctor);
                        }
                        catch (KeyNotFoundException ex)
                        {
                            return Results.Problem("No se encontro el registro.");
                        }
                    }
                 )
                .WithSummary("Consultar Doctor")
                .WithName("Consultar Doctor");

                app.MapPost("api/doctores", async (RegistrarDoctorRequest request, DoctorService doctorService) =>
                {
                    var result = await doctorService.RegistrarDoctor(request.Nombre, request.Especialidad);
                    return Results.Created($"/api/doctores/{result.Id}", result);
                })
                .WithName("RegistrarDoctor")
                .WithSummary("Registrar Doctor");

                app.MapPut("api/doctores/{doctorId}", async (Guid doctorId, RegistrarDoctorRequest request, DoctorService doctorService) =>
                {
                    var updated = await doctorService.ActualizarDoctor(doctorId, request.Nombre, request.Especialidad);
                    return Results.Ok(updated);
                })
                .WithName("ActualizarDoctor")
                .WithSummary("Actualizar Doctor");

                app.MapDelete("api/doctores/{doctorId}", async (Guid doctorId, DoctorService doctorService) =>
                {
                    await doctorService.EliminarDoctor(doctorId);
                    return Results.NoContent();
                })
                .WithName("EliminarDoctor")
                .WithSummary("Eliminar Doctor");

                app.MapGet("api/pacientes/{pacienteId}",
                    async (
                        [FromRoute] Guid pacienteId,
                        PacienteService pacienteService,
                        CancellationToken ct
                        ) =>
                    {
                        try
                        {
                            var paciente = await pacienteService.ConsultarPaciente(pacienteId);
                            return Results.Ok(paciente);
                        }
                        catch (KeyNotFoundException ex)
                        {
                            return Results.Problem("No se encontro el registro.");
                        }
                    }
                 )
                .WithSummary("Consultar Paciente")
                .WithName("Consultar Paciente");

                app.MapPost("api/pacientes", async (RegistrarPacienteRequest request, PacienteService pacienteService) =>
                {
                    var result = await pacienteService.RegistrarPaciente(request.Nombre, request.Condicion);
                    return Results.Created($"/api/pacientes/{result.Id}", result);
                })
                .WithName("RegistrarPaciente")
                .WithSummary("Registrar Paciente");

                app.MapPut("api/pacientes/{pacienteId}", async (Guid pacienteId, RegistrarPacienteRequest request, PacienteService pacienteService) =>
                {
                    var updated = await pacienteService.ActualizarPaciente(pacienteId, request.Nombre, request.Condicion);
                    return Results.Ok(updated);
                })
                .WithName("ActualizarPaciente")
                .WithSummary("Actualizar Paciente");

                app.MapDelete("api/pacientes/{pacienteId}", async (Guid pacienteId, PacienteService pacienteService) =>
                {
                    await pacienteService.EliminarPaciente(pacienteId);
                    return Results.NoContent();
                })
                .WithName("EliminarPaciente")
                .WithSummary("Eliminar Paciente");

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
