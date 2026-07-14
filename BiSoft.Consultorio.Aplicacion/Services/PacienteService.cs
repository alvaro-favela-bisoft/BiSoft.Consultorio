using BiSoft.Consultorio.Aplicacion.DTOs.Doctor;
using BiSoft.Consultorio.Dominio.Services;
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
    }
}
