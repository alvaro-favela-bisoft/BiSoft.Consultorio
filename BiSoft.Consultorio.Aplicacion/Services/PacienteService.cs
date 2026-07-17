using BiSoft.Consultorio.Aplicacion.DTOs.Paciente;
using BiSoft.Consultorio.Dominio.Services;
using Mapster;
using Microsoft.Extensions.Logging;

namespace BiSoft.Consultorio.Aplicacion.Services
{
    public class PacienteService
    {
        private readonly ILogger<PacienteService> _logger;
        private readonly PacienteDomainService _pacienteDomainService;

        public PacienteService(
            ILogger<PacienteService> logger,
            PacienteDomainService pacienteDomainService)
        {
            _logger = logger;
            _pacienteDomainService = pacienteDomainService;
        }

        public async Task<RegistrarPacienteResponse> RegistrarPaciente(
            string nombre,
            string condicion)
        {
            var paciente = await _pacienteDomainService
                .RegistrarPaciente(nombre, condicion);

            return paciente.Adapt<RegistrarPacienteResponse>();
        }
        public async Task<ConsultarPacienteResponse> ConsultarPaciente(Guid pacienteId)
        {
            var paciente = await _pacienteDomainService.ObtenerPaciente(pacienteId);
            return paciente.Adapt<ConsultarPacienteResponse>(); 
        }

        public async Task<RegistrarPacienteResponse> ActualizarPaciente(Guid pacienteId, string nombre, string condicion)
        {
            var paciente = await _pacienteDomainService.ActualizarPaciente(pacienteId, nombre, condicion);
            return paciente.Adapt<RegistrarPacienteResponse>();
        }

        public async Task EliminarPaciente(Guid pacienteId)
        {
            await _pacienteDomainService.EliminarPaciente(pacienteId);
        }

        public async Task<object?> RestaurarPaciente(Guid pacienteId)
        {
            await _pacienteDomainService.RestaurarPaciente(pacienteId);
            var paciente = await _pacienteDomainService.ObtenerPaciente(pacienteId);
            _logger.LogInformation("Paciente restaurado: {PacienteId}", pacienteId);
            return paciente.Adapt<ConsultarPacienteResponse>();
        }
    }
}
