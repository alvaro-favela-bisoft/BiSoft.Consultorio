using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;
using BiSoft.Consultorio.Infraestructura.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BiSoft.Consultorio.Infraestructura.Repositories.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuariosContext _context;

        public UsuarioRepository(UsuariosContext context)
        {
            _context = context;
        }

        public async Task<Usuario> RegistrarUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario?> ObtenerUsuario(Guid id)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario?> ObtenerUsuario(string usuario)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(x => x.UsuarioLogin == usuario);
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task ActualizarUsuario(Usuario usuario)
        {
            _context.Update(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task EliminarUsuario(Guid id)
        {
            var usuario = await ObtenerUsuario(id);

            if (usuario == null)
                return;

            _context.Remove(usuario);

            await _context.SaveChangesAsync();
        }
    }
}