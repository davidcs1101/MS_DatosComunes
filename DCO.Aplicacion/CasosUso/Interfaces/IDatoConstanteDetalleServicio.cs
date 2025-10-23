using DCO.Dtos;

namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IDatoConstanteDetalleServicio
    {
        Task<ApiResponse<int>> CrearAsync(DatoConstanteDetalleCreacionRequest datoConstanteDetalleCreacionRequest);
        Task<ApiResponse<string>> ModificarAsync(DatoConstanteDetalleModificacionRequest datoConstanteDetalleModificacionRequest);
    }
}
