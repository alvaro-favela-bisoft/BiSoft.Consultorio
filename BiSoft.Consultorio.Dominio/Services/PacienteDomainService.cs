using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BiSoft.Consultorio.Dominio.Services
{
    public class PacienteDomainService
    {
        private readonly ILogger<PacienteDomainService> _logger;
        private readonly IPacienteRepository _pacienteRepository;
        public PacienteDomainService(ILogger<PacienteDomainService> logger, IPacienteRepository pacienteRepository)
        {
            _logger = logger;
            _pacienteRepository = pacienteRepository;
        }
        public async Task<Paciente> RegistrarPaciente(string nombre, string condicion)
        {
            var paciente = new Paciente(nombre, condicion);
            await _pacienteRepository.RegistrarPaciente(paciente);
            await _pacienteRepository.GuardarCambios();
            _logger.LogInformation("Paciente registrado: {PacienteNombre}", paciente.Nombre);
            return paciente;
        }
        public async Task<Paciente> ActualizarPaciente(
            Guid pacienteId,
            string nombre,
            string condicion)
        {
            var paciente = await ObtenerPaciente(pacienteId);
            paciente.Actualizar(nombre, condicion);
            await _pacienteRepository.GuardarCambios();
            _logger.LogInformation("Pacientes actualizado: {DoctorNombre}, Condicion: {DoctorEspecialidad}", paciente.Nombre, paciente.Condicion);
            return paciente;
        }
        public async Task<Paciente> ObtenerPaciente(Guid pacienteId)
        {
            var paciente = await _pacienteRepository.ObtenerPaciente(pacienteId) ?? throw new Exception($"No se encontro el paciente con id {pacienteId}");
            _logger.LogInformation("Paciente obtenido: {PacienteNombre}", paciente.Nombre);
            return paciente;
        }
        public IQueryable<Paciente> ConsultarPacientes()
        {
            var pacientes = _pacienteRepository.ConsultarPaciente();
            _logger.LogInformation("Consulta de pacientes realizada.");
            return pacientes;
        }
        public async Task EliminarPaciente(Guid pacienteId)
        {
            var paciente = await ObtenerPaciente(pacienteId);
            await _pacienteRepository.EliminarPaciente(paciente);
            await _pacienteRepository.GuardarCambios();
            _logger.LogInformation("Paciente eliminado: {PacienteId}", pacienteId);
        }
        public async Task RestaurarPaciente(Guid pacienteId)
        {
            var pacientesEliminados = await _pacienteRepository.ObtenerPacientesEliminados();
            var paciente = pacientesEliminados.FirstOrDefault(p => p.Id == pacienteId)
                ?? throw new KeyNotFoundException($"No se encontró paciente eliminado con id {pacienteId}");

            await _pacienteRepository.RestaurarPaciente(paciente);
            await _pacienteRepository.GuardarCambios();
            _logger.LogInformation("Paciente restaurado: {PacienteId}", pacienteId);
        }
    }
}
