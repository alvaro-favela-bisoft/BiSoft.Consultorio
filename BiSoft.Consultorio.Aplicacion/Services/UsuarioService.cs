using BCrypt.Net;
using BiSoft.Consultorio.Aplicacion.DTOs.Usuario;
using BiSoft.Consultorio.Dominio.Entidades;
using BiSoft.Consultorio.Dominio.Repositories;

namespace BiSoft.Consultorio.Aplicacion.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<RegistrarUsuarioResponse> RegistrarUsuario(RegistrarUsuarioRequest request)
        {
            var existe = await _usuarioRepository.ObtenerUsuario(request.Usuario);

            if (existe != null)
                throw new Exception("El usuario ya existe.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var usuario = new Usuario(
                request.Nombre,
                request.Usuario,
                passwordHash
            );

            await _usuarioRepository.RegistrarUsuario(usuario);

            return new RegistrarUsuarioResponse(
                usuario.Id,
                usuario.Nombre,
                usuario.UsuarioLogin
            );
        }
        public async Task<ConsultarUsuarioResponse> ConsultarUsuario(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObtenerUsuario(usuarioId);

            if (usuario == null)
                throw new Exception("El usuario no existe.");

            return new ConsultarUsuarioResponse(
                usuario.Id,
                usuario.Nombre,
                usuario.UsuarioLogin
            );
        }

        public async Task<ActualizarUsuarioResponse> ActualizarUsuario(Guid usuarioId, ActualizarUsuarioRequest request)
        {
            var usuario = await _usuarioRepository.ObtenerUsuario(usuarioId);

            if (usuario == null)
                throw new Exception("El usuario no existe.");

            usuario.Actualizar(request.Nombre, request.Usuario);

            await _usuarioRepository.ActualizarUsuario(usuario);

            return new ActualizarUsuarioResponse(
                usuario.Id,
                usuario.Nombre,
                usuario.UsuarioLogin
            );
        }
        public async Task EliminarUsuario(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObtenerUsuario(usuarioId);

            if (usuario == null)
                throw new Exception("El usuario no existe.");

            await _usuarioRepository.EliminarUsuario(usuarioId);
        }
        public async Task<Usuario?> ValidarUsuario(string usuarioLogin, string password)
        {
            var usuario = await _usuarioRepository.ObtenerUsuario(usuarioLogin);

            if (usuario == null)
                return null;

            bool passwordCorrecto =
                BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash);

            if (!passwordCorrecto)
                return null;

            return usuario;
        }
    }
}