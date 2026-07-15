using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Infraestructura.Repositories.Consultorio
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ConsultorioContext _context;
        public PacienteRepository(ConsultorioContext context)
        {
            _context = context;
        }

        public IQueryable<Paciente?> ConsultarPaciente()
        {
            return _context.Pacientes.AsQueryable();
        }

        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<Paciente?> ObtenerPaciente(Guid pacienteId)
        {
            return await _context.Pacientes.OrderBy(p => p.Id).FirstOrDefaultAsync(p => p.Id == pacienteId);
        }

        public Task RegistrarPaciente(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            return Task.CompletedTask;
        }

        public Task EliminarPaciente(Paciente paciente)
        {
            _context.Pacientes.Remove(paciente);
            return Task.CompletedTask;
        }
    }
}
