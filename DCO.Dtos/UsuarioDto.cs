using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public int TipoIdentificacionId { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombre1 { get; set; } = null!;
        public string? Nombre2 { get; set; }
        public string Apellido1 { get; set; } = null!;
        public string? Apellido2 { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public bool CambiarClave { get; set; }

        public int UsuarioCreadorId { get; set; }
        public string? NombreUsuarioCreador { get; set; }
        public DateTime FechaCreado { get; set; }
        public int? UsuarioModificadorId { get; set; }
        public string? NombreUsuarioModificador { get; set; }
        public DateTime? FechaModificado { get; set; }
        public bool EstadoActivo { get; set; }
    }
}
