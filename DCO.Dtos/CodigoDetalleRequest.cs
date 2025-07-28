using System.ComponentModel.DataAnnotations;
using Utilidades;

namespace DCO.Dtos
{
    public class CodigoDetalleRequest
    {
        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        [MaxLength(30, ErrorMessage = Textos.Generales.VALIDA_VALOR_EXCEDE_LONGITUD)]
        public string Codigo { get; set; } = null!;

        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        [MaxLength(30, ErrorMessage = Textos.Generales.VALIDA_VALOR_EXCEDE_LONGITUD)]
        public string CodigoListaDetalle { get; set; } = null!;
    }
}
