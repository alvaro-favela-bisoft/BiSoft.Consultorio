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
            try {
                var builder = WebApplication.CreateBuilder(args);
                var conncectionStrings = builder.Configuration["DatabaseConnections:Consultorio:ConnectionStrings"];

                builder.Services.AddScoped<DoctorService>();
                builder.Services.AddScoped<DoctorDomainService>();
                builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
                builder.Services.AddScoped<PacienteService>();
                builder.Services.AddScoped<PacienteDomainService>();
                builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
                //Inyeccion de contextos
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

                //OpenAPI
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                //CORS
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

                app.MapEndpoints();

                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseMiddleware<ErrorHandlerMiddleware>();

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
