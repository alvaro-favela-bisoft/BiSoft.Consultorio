using BiSoft.Consultorio.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Repositories
{
    public interface IDoctorRepository
    {
        Task RegistrarDoctor(Doctor doctor);
        Task GuardarCambios();
        Task<Doctor?> ObtenerDoctor(Guid doctorId);
        Task<List<Doctor>> ObtenerDoctoresEliminados();
        Task RestaurarDoctor(Doctor doctor);
        IQueryable<Doctor?> ConsultarDoctor();
        Task EliminarDoctor(Doctor doctor);
    }
}
