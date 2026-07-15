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
        Task GuardarCambios();
        Task<Paciente?> ObtenerPaciente(Guid pacienteId);
        IQueryable<Paciente?> ConsultarPaciente();
        Task EliminarPaciente(Paciente paciente);
    }
}
