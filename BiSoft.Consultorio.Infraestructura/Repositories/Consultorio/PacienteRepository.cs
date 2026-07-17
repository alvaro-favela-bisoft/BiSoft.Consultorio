using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Contexts;
using Microsoft.EntityFrameworkCore;

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
            return _context.Pacientes;
        }
        public Task RegistrarPaciente(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            return Task.CompletedTask;
        }

        public async Task<Paciente?> ObtenerPaciente(Guid pacienteId)
        {
            return await _context.Pacientes
                .FirstOrDefaultAsync(p => p.Id == pacienteId);
        }

        public async Task<List<Paciente>> ObtenerPacientesEliminados()
        {
            return await _context.Pacientes
                .IgnoreQueryFilters()
                .Where(p => p.IsDeleted)
                .ToListAsync();
        }

        public Task EliminarPaciente(Paciente paciente)
        {
            paciente.Eliminar();
            _context.Pacientes.Update(paciente);
            return Task.CompletedTask;
        }

        public Task RestaurarPaciente(Paciente paciente)
        {
            paciente.Restaurar();
            _context.Pacientes.Update(paciente);
            return Task.CompletedTask;
        }

        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }
    }
}
