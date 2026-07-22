using System;
using System.Collections.Generic;
using System.Text;

namespace BiSoft.Consultorio.Aplicacion.DTOs.Usuario
{
    public record RegistrarUsuarioResponse
    (
    Guid Id,

    string Nombre,

    string Usuario
    );
}