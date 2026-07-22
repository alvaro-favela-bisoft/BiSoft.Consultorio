using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.DTOs.Usuario
{
    public record ActualizarUsuarioRequest
    (
        string Nombre,
        string Usuario
    );
}
