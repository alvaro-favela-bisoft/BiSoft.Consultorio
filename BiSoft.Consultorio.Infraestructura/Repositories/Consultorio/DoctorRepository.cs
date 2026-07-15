using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
            return _context.Doctores.AsQueryable();
        }

        public Task GuardarCambios()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<Doctor?> ObtenerDoctor(Guid doctorId)
        {
            return await _context.Doctores.OrderBy( d => d.Id ).FirstOrDefaultAsync(d => d.Id == doctorId);
        }

        public Task RegistrarDoctor(Doctor doctor)
        {
            _context.Doctores.Add(doctor);
            return Task.CompletedTask;
        }
        public Task EliminarDoctor(Doctor doctor)
        {
            _context.Doctores.Remove(doctor);
            return Task.CompletedTask;
        }
    }
}
