using BiSoft.Consultorio.Dominio.Validators.Entidades;
using BiSoft.Consultorio.Dominio.Validators.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades.Base
{
    public abstract class Persona
    {
        public Guid Id { get; }
        public string Nombre { get; private set; }
        public bool IsDeleted { get; set; } = false;
        private DateTime? _deletedAt;

        public DateTime? DeletedAt
        {
            get => _deletedAt;
            set => _deletedAt = value;
        }
        protected Persona() { }
        protected Persona(string nombre)
        {
            Id = Guid.NewGuid();
            Nombre = nombre.ValidateNombre();
            IsDeleted = false;
        }
        public void Actualizar(string nombre)
        {
            Nombre = nombre.ValidateNombre();
        }

        // Metodos para Soft Delete
        public void Eliminar()
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }

        public void Restaurar()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
