using BiSoft.Consultorio.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Repositories
{
    public interface ICitaRepository
    {
        Task RegistrarCita(Cita cita);
        Task<Cita?> ObtenerCita(Guid citaId);
        Task<List<Cita>> ObtenerCitasPorSalaYFecha(Guid salaId, DateTime fecha);
        Task<List<Cita>> ObtenerCitasPorDoctorYFecha(Guid doctorId, DateTime fecha);
        Task<List<Cita>> ObtenerCitasEliminadas();
        Task ActualizarCita(Cita cita);
        Task EliminarCita(Cita cita);
        Task RestaurarCita(Cita cita);
        Task GuardarCambios();
    }
}
