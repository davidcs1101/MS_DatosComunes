using System.ComponentModel.DataAnnotations;
using Utilidades;

namespace DCO.Dtos
{
    public class CodigoIdListaDetalleRequest
    {
        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        [MaxLength(30, ErrorMessage = Textos.Generales.VALIDA_VALOR_EXCEDE_LONGITUD)]
        public string CodigoLista { get; set; } = null!;

        [Required(ErrorMessage = Textos.Generales.VALIDA_CAMPO_OBLIGATORIO)]
        public int Id { get; set; }
    }
}
