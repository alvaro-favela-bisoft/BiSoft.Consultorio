using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Sala
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }

        // Metodos para el Soft Delete
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
