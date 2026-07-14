using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Validators.Entidades
{
    public static class DoctorValidator
    {
        public static string ValidateEspecialidad(this string especialidad)
        {
            var especialidadParametro = "especialidad";
            especialidad = especialidad.Trim()
                                       .ValidateEmptyOrWhitespace(especialidadParametro)
                                       .ValidateLength(especialidadParametro, 5, 100);

            return especialidad;
        }
    }
}
