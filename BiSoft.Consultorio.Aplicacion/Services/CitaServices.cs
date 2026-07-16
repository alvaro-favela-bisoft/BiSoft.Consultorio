using BiSoft.Consultorio.Aplicacion.DTOs.Cita;
using BiSoft.Consultorio.Dominio.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.Services
{
    public class CitaService
    {
        private readonly ILogger<CitaService> _logger;
        private readonly CitaDomainService _citaDomainService;

        public CitaService(ILogger<CitaService> logger, CitaDomainService citaDomainService)
        {
            _logger = logger;
            _citaDomainService = citaDomainService;
        }

        public async Task<RegistrarCitaResponse> RegistrarCita(DateTime fecha, Guid pacienteId, Guid doctorId, Guid salaId, string motivo)
        {
            var cita = await _citaDomainService.RegistrarCita(fecha, pacienteId, doctorId, salaId, motivo);
            return MapearCitaResponse(cita);
        }

        public async Task<RegistrarCitaResponse> ObtenerCita(Guid citaId)
        {
            var cita = await _citaDomainService.ObtenerCita(citaId);
            return MapearCitaResponse(cita);
        }

        public async Task<RegistrarCitaResponse> ActualizarCita(Guid citaId, DateTime fecha, string motivo)
        {
            var cita = await _citaDomainService.ActualizarCita(citaId, fecha, motivo);
            return MapearCitaResponse(cita);
        }

        public async Task EliminarCita(Guid citaId)
        {
            await _citaDomainService.EliminarCita(citaId);
        }

        private RegistrarCitaResponse MapearCitaResponse(BiSoft.Consultorio.Dominio.Entidades.Cita cita)
        {
            return new RegistrarCitaResponse
            {
                Id = cita.Id,
                Fecha = cita.Fecha,
                PacienteId = cita.PacienteId,
                PacienteNombre = cita.Paciente?.Nombre ?? "N/A",
                DoctorId = cita.DoctorId,
                DoctorNombre = cita.Doctor?.Nombre ?? "N/A",
                SalaId = cita.SalaId,
                SalaNombre = cita.Sala?.Nombre ?? "N/A",
                Motivo = cita.Motivo
            };
        }
    }
}
