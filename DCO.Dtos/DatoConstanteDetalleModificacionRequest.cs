using System.ComponentModel.DataAnnotations;
using Utilidades;

namespace DCO.Dtos
{
    public class DatoConstanteDetalleModificacionRequest
    {
        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public int Id { get; set; }

        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public bool EstadoActivo { get; set; }
    }
}
