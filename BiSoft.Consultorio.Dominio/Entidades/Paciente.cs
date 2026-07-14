using BiSoft.Consultorio.Dominio.Validators.Entidades;
using BiSoft.Consultorio.Dominio.Validators;
using System;
using BiSoft.Consultorio.Dominio.Entidades.Base;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Paciente : Persona
    {
        private Paciente() { }
        public Paciente(string nombre, string condicion) : base(nombre)
        {
        }
    }
}
