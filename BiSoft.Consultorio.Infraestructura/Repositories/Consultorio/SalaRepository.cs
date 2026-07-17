// csharp BiSoft.Consultorio.Infraestructura\Repositories\Consultorio\SalaRepository.cs
using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BiSoft.Consultorio.Infraestructura.Repositories.Consultorio
{
    public class SalaRepository : ISalaRepository
    {
        private readonly ConsultorioContext _context;

        public SalaRepository(ConsultorioContext context)
        {
            _context = context;
        }

        public Task RegistrarSala(Sala sala)
        {
            _context.Salas.Add(sala);
            return Task.CompletedTask;
        }

        public async Task<Sala?> ObtenerSala(Guid salaId)
        {
            return await _context.Salas.FirstOrDefaultAsync(s => s.Id == salaId);
        }
        public async Task<List<Sala>> ObtenerSalasEliminadas()
        {
            return await _context.Salas
                .IgnoreQueryFilters()
                .Where(s => s.IsDeleted)
                .ToListAsync();
        }

        public Task ActualizarSala(Sala sala)
        {
            _context.Salas.Update(sala);
            return Task.CompletedTask;
        }

        public Task EliminarSala(Sala sala)
        {
            sala.Eliminar();
            _context.Salas.Update(sala);
            return Task.CompletedTask;
        }
        public Task RestaurarSala(Sala sala)
        {
            sala.Restaurar();
            _context.Salas.Update(sala);
            return Task.CompletedTask;
        }
        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }
    }
}