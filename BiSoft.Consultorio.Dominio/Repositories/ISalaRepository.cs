using BiSoft.Consultorio.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Repositories
{
    public interface ISalaRepository
    {
        Task RegistrarSala(Sala sala);
        Task<Sala?> ObtenerSala(Guid salaId);
        Task ActualizarSala(Sala sala);
        Task EliminarSala(Sala sala);
        Task GuardarCambios();
    }
}
