using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Dominio.Entidades
{
    public class DCO_BaseAuditoria
    {
        public int UsuarioCreadorId { get; set; }
        public DateTime FechaCreado { get; set; }
        public int? UsuarioModificadorId { get; set; }
        public DateTime? FechaModificado { get; set; }
    }
}
