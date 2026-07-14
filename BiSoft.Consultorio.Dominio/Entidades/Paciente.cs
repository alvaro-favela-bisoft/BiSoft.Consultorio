using BiSoft.Consultorio.Dominio.Entidades.Base;
using BiSoft.Consultorio.Dominio.Validators.Entidades;

namespace BiSoft.Consultorio.Dominio.Entidades
{
    public class Paciente : Persona
    {
        public string Condicion { get; private set; }

        private Paciente() : base() { }

        public Paciente(string nombre, string condicion) : base(nombre)
        {
            Condicion = condicion.ValidateCondicion();
        }

        public void Actualizar(string nombre, string condicion)
        {
            base.Actualizar(nombre);
            Condicion = condicion.ValidateCondicion();
        }
    }
}
