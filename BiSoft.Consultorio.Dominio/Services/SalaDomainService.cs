using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Services
{
    public class SalaDomainService
    {
        private readonly ILogger<SalaDomainService> _logger;
        private readonly ISalaRepository _salaRepository;

        public SalaDomainService(ILogger<SalaDomainService> logger, ISalaRepository salaRepository)
        {
            _logger = logger;
            _salaRepository = salaRepository;
        }

        public async Task<Sala> RegistrarSala(string nombre)
        {
            var sala = new Sala { Id = Guid.NewGuid(), Nombre = nombre };
            await _salaRepository.RegistrarSala(sala);
            await _salaRepository.GuardarCambios();
            _logger.LogInformation("Sala registrada: {SalaNombre}", nombre);
            return sala;
        }

        public async Task<Sala> ObtenerSala(Guid salaId)
        {
            var sala = await _salaRepository.ObtenerSala(salaId)
                ?? throw new KeyNotFoundException($"No se encontró la sala con id {salaId}");
            return sala;
        }

        public async Task<Sala> ActualizarSala(Guid salaId, string nombre)
        {
            var sala = await ObtenerSala(salaId);
            sala.Nombre = nombre;
            await _salaRepository.ActualizarSala(sala);
            await _salaRepository.GuardarCambios();
            _logger.LogInformation("Sala actualizada: {SalaNombre}", nombre);
            return sala;
        }

        public async Task EliminarSala(Guid salaId)
        {
            var sala = await ObtenerSala(salaId);
            await _salaRepository.EliminarSala(sala);
            await _salaRepository.GuardarCambios();
            _logger.LogInformation("Sala eliminada: {SalaId}", salaId);
        }
        public async Task RestaurarSala(Guid salaId)
        {
            var salasEliminadas = await _salaRepository.ObtenerSalasEliminadas();
            var sala = salasEliminadas.FirstOrDefault(s => s.Id == salaId)
                ?? throw new KeyNotFoundException($"No se encontró sala eliminada con id {salaId}");

            await _salaRepository.RestaurarSala(sala);
            await _salaRepository.GuardarCambios();
            _logger.LogInformation("Sala restaurada: {SalaId}", salaId);
        }
    }
}
