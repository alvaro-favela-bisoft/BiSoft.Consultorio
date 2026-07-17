using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Contexts;
using Microsoft.EntityFrameworkCore;


namespace BiSoft.Consultorio.Infraestructura.Repositories.Consultorio
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ConsultorioContext _context;

        public DoctorRepository(ConsultorioContext context)
        {
            _context = context;
        }
        public IQueryable<Doctor?> ConsultarDoctor()
        {
            return _context.Doctores;
        }
        public async Task<Doctor?> ObtenerDoctor(Guid doctorId)
        {
            return await _context.Doctores
                .FirstOrDefaultAsync(d => d.Id == doctorId);
        }

        public async Task<List<Doctor>> ObtenerDoctoresEliminados()
        {
            return await _context.Doctores
                .IgnoreQueryFilters()  // IGNORA FILTRO GLOBAL
                .Where(d => d.IsDeleted)
                .ToListAsync();
        }

        public Task RegistrarDoctor(Doctor doctor)
        {
            _context.Doctores.Add(doctor);
            return Task.CompletedTask;
        }

        public Task EliminarDoctor(Doctor doctor)
        {
            // SOFT DELETE
            doctor.Eliminar();
            _context.Doctores.Update(doctor);
            return Task.CompletedTask;
        }

        public Task RestaurarDoctor(Doctor doctor)
        {
            // RESTAURAR
            doctor.Restaurar();
            _context.Doctores.Update(doctor);
            return Task.CompletedTask;
        }

        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }

    }
}
