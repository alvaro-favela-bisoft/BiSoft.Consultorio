using BiSoft.Consultorio.Aplicacion.DTOs.Sala;
using BiSoft.Consultorio.Dominio.Services;
using Mapster;
using Microsoft.Extensions.Logging;

namespace BiSoft.Consultorio.Aplicacion.Services
{
    public class SalaService
    {
        private readonly ILogger<SalaService> _logger;
        private readonly SalaDomainService _salaDomainService;

        public SalaService(ILogger<SalaService> logger, SalaDomainService salaDomainService)
        {
            _logger = logger;
            _salaDomainService = salaDomainService;
        }

        public async Task<RegistrarSalaResponse> RegistrarSala(string nombre)
        {
            var sala = await _salaDomainService.RegistrarSala(nombre);
            return sala.Adapt<RegistrarSalaResponse>();
        }

        public async Task<RegistrarSalaResponse> ObtenerSala(Guid salaId)
        {
            var sala = await _salaDomainService.ObtenerSala(salaId);
            return sala.Adapt<RegistrarSalaResponse>();
        }

        public async Task<RegistrarSalaResponse> ActualizarSala(Guid salaId, string nombre)
        {
            var sala = await _salaDomainService.ActualizarSala(salaId, nombre);
            return sala.Adapt<RegistrarSalaResponse>();
        }

        public async Task EliminarSala(Guid salaId)
        {
            await _salaDomainService.EliminarSala(salaId);
        }
    }
}
