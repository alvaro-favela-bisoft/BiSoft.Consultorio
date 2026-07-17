using BiSoft.Consultorio.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Repositories
{
    public interface IPacienteRepository
    {
        Task RegistrarPaciente(Paciente paciente);
        Task<Paciente?> ObtenerPaciente(Guid pacienteId);
        Task<List<Paciente>> ObtenerPacientesEliminados();
        Task EliminarPaciente(Paciente paciente);
        Task RestaurarPaciente(Paciente paciente);
        Task GuardarCambios();
        IQueryable<Paciente> ConsultarPaciente();
    }
}
