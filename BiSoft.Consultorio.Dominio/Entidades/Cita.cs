using System;
namespace BiSoft.Consultorio.Dominio.Entidades 
{ 
    public class Cita 
    { 
        public Guid Id { get; set; } 
        public DateTime Fecha { get; set; } 
        public Guid PacienteId { get; set; } 
        public Paciente Paciente { get; set; } 
        public Guid DoctorId { get; set; } 
        public Doctor Doctor { get; set; } 
        public string Motivo { get; set; } 
        public string Diagnostico { get; set; } 
        public Guid SalaId { get; set; } 
        public Sala Sala { get; set; } 
    } 
}
