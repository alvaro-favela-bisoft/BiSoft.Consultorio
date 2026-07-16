using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BiSoft.Consultorio.Infraestructura.Repositories.Consultorio
{
    public class CitaRepository : ICitaRepository
    {
        private readonly ConsultorioContext _context;

        public CitaRepository(ConsultorioContext context)
        {
            _context = context;
        }

        public Task RegistrarCita(Cita cita)
        {
            _context.Citas.Add(cita);
            return Task.CompletedTask;
        }

        public async Task<Cita?> ObtenerCita(Guid citaId)
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Include(c => c.Sala)
                .FirstOrDefaultAsync(c => c.Id == citaId);
        }

        public async Task<List<Cita>> ObtenerCitasPorSalaYFecha(Guid salaId, DateTime fecha)
        {
            var fechaInicio = fecha.Date;
            var fechaFin = fecha.Date.AddDays(1);

            return await _context.Citas
                .Where(c => c.SalaId == salaId && c.Fecha >= fechaInicio && c.Fecha < fechaFin)
                .ToListAsync();
        }

        public async Task<List<Cita>> ObtenerCitasPorDoctorYFecha(Guid doctorId, DateTime fecha)
        {
            var fechaInicio = fecha.Date;
            var fechaFin = fecha.Date.AddDays(1);

            return await _context.Citas
                .Where(c => c.DoctorId == doctorId && c.Fecha >= fechaInicio && c.Fecha < fechaFin)
                .ToListAsync();
        }

        public Task ActualizarCita(Cita cita)
        {
            _context.Citas.Update(cita);
            return Task.CompletedTask;
        }

        public Task EliminarCita(Cita cita)
        {
            _context.Citas.Remove(cita);
            return Task.CompletedTask;
        }

        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }
    }
}
