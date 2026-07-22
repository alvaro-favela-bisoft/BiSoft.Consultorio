using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.DTOs.Usuario
{
    public record ActualizarUsuarioResponse
    (
        Guid Id,
        string Nombre,
        string Usuario
    );
}
