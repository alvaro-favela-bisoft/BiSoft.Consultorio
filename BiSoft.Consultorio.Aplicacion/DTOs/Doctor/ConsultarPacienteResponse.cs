using System;

namespace BiSoft.Consultorio.Aplicacion.DTOs.Doctor
{
    public class ConsultarPacienteResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Condicion { get; set; } = string.Empty;
    }
}
