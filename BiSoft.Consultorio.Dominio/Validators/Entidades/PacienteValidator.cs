using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Dominio.Validators.Entidades
{
    public static class PacienteValidator
    {
        public static string ValidateCondicion(this string condicion)
        {
            var parametro = "condicion";

            condicion = condicion.Trim()
                                 .ValidateEmptyOrWhitespace(parametro)
                                 .ValidateLength(parametro, 5, 100);

            return condicion;
        }
    }
}
