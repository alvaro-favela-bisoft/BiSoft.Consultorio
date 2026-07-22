using System;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Usuario
    {
        public Guid Id { get; private set; }

        public string Nombre { get; private set; }

        public string UsuarioLogin { get; private set; }

        public string PasswordHash { get; private set; }

        public bool Activo { get; private set; }

        private Usuario()
        {
        }

        public Usuario(
            string nombre,
            string usuarioLogin,
            string passwordHash)
        {
            Id = Guid.NewGuid();

            Nombre = nombre;

            UsuarioLogin = usuarioLogin;

            PasswordHash = passwordHash;

            Activo = true;
        }

        public void Actualizar(
            string nombre,
            string usuarioLogin)
        {
            Nombre = nombre;

            UsuarioLogin = usuarioLogin;
        }

        public void CambiarPassword(string passwordHash)
        {
            PasswordHash = passwordHash;
        }

        public void Eliminar()
        {
            Activo = false;
        }

        public void Restaurar()
        {
            Activo = true;
        }
    }
}
