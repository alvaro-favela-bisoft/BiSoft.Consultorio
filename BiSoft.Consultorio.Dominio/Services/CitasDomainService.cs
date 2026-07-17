using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Services
{
    public class CitaDomainService
    {
        private readonly ILogger<CitaDomainService> _logger;
        private readonly ICitaRepository _citaRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPacienteRepository _pacienteRepository;
        private readonly ISalaRepository _salaRepository;

        public CitaDomainService(
            ILogger<CitaDomainService> logger,
            ICitaRepository citaRepository,
            IDoctorRepository doctorRepository,
            IPacienteRepository pacienteRepository,
            ISalaRepository salaRepository)
        {
            _logger = logger;
            _citaRepository = citaRepository;
            _doctorRepository = doctorRepository;
            _pacienteRepository = pacienteRepository;
            _salaRepository = salaRepository;
        }

        public async Task<Cita> RegistrarCita(DateTime fecha, Guid pacienteId, Guid doctorId, Guid salaId, string motivo)
        {
            // Validar que el paciente existe
            var paciente = await _pacienteRepository.ObtenerPaciente(pacienteId)
                ?? throw new KeyNotFoundException("Paciente no encontrado");

            // Validar que el doctor existe
            var doctor = await _doctorRepository.ObtenerDoctor(doctorId)
                ?? throw new KeyNotFoundException("Doctor no encontrado");

            // Validar que la sala existe
            var sala = await _salaRepository.ObtenerSala(salaId)
                ?? throw new KeyNotFoundException("Sala no encontrada");

            // Validar que la sala no esté ocupada en la fecha/hora
            var citasSala = await _citaRepository.ObtenerCitasPorSalaYFecha(salaId, fecha);
            if (citasSala.Any(c => c.Fecha == fecha))
            {
                throw new InvalidOperationException("La sala está ocupada en ese horario");
            }

            // Validar que el doctor no tenga otra cita en la misma fecha/hora
            var citasDoctor = await _citaRepository.ObtenerCitasPorDoctorYFecha(doctorId, fecha);
            if (citasDoctor.Any(c => c.Fecha == fecha))
            {
                throw new InvalidOperationException("El doctor no está disponible en ese horario");
            }

            var cita = new Cita
            {
                Id = Guid.NewGuid(),
                Fecha = fecha,
                PacienteId = pacienteId,
                DoctorId = doctorId,
                SalaId = salaId,
                Motivo = motivo,
                Diagnostico = string.Empty
            };

            await _citaRepository.RegistrarCita(cita);
            await _citaRepository.GuardarCambios();
            _logger.LogInformation("Cita registrada: {CitaId} - Doctor: {DoctorId}, Paciente: {PacienteId}, Sala: {SalaId}", cita.Id, doctorId, pacienteId, salaId);
            return cita;
        }

        public async Task<Cita> ObtenerCita(Guid citaId)
        {
            var cita = await _citaRepository.ObtenerCita(citaId)
                ?? throw new KeyNotFoundException($"No se encontró la cita con id {citaId}");
            return cita;
        }

        public async Task<Cita> ActualizarCita(Guid citaId, DateTime fecha, string motivo)
        {
            var cita = await ObtenerCita(citaId);
            cita.Fecha = fecha;
            cita.Motivo = motivo;
            await _citaRepository.ActualizarCita(cita);
            await _citaRepository.GuardarCambios();
            _logger.LogInformation("Cita actualizada: {CitaId}", citaId);
            return cita;
        }

        public async Task EliminarCita(Guid citaId)
        {
            var cita = await ObtenerCita(citaId);
            await _citaRepository.EliminarCita(cita);
            await _citaRepository.GuardarCambios();
            _logger.LogInformation("Cita eliminada: {CitaId}", citaId);
        }
        public async Task RestaurarCita(Guid citaId)
        {
            var citasEliminadas = await _citaRepository.ObtenerCitasEliminadas();
            var cita = citasEliminadas.FirstOrDefault(c => c.Id == citaId)
                ?? throw new KeyNotFoundException($"No se encontró cita eliminada con id {citaId}");

            await _citaRepository.RestaurarCita(cita);
            await _citaRepository.GuardarCambios();
            _logger.LogInformation("Cita restaurada: {CitaId}", citaId);
        }
    }
}
