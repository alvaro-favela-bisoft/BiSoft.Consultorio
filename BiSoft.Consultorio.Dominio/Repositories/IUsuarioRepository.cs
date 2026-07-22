using BiSoft.Consultorio.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiSoft.Consultorio.Dominio.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> RegistrarUsuario(Usuario usuario);

        Task<Usuario?> ObtenerUsuario(Guid id);

        Task<Usuario?> ObtenerUsuario(string usuario);

        Task<IEnumerable<Usuario>> ObtenerUsuarios();

        Task ActualizarUsuario(Usuario usuario);

        Task EliminarUsuario(Guid id);
    }
}
