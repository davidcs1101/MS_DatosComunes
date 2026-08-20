using DCO.Dtos;
using Utilidades.Dtos;
namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IDatoConstanteDetalleServicio
    {
        Task<ApiResponseDto<int>> CrearAsync(DatoConstanteDetalleCreacionRequest datoConstanteDetalleCreacionRequest);
        Task<ApiResponseDto<string>> ModificarAsync(DatoConstanteDetalleModificacionRequest datoConstanteDetalleModificacionRequest);
    }
}
