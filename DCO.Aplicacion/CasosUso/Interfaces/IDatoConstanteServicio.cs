using DCO.Dtos;
using Utilidades.Dtos;
namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IDatoConstanteServicio
    {
        Task<ApiResponseDto<int>> CrearAsync(DatoConstanteCreacionRequest datoConstanteCreacionRequest);
        Task<ApiResponseDto<string>> ModificarAsync(DatoConstanteModificacionRequest datoConstanteModificacionRequest);
        Task<ApiResponseDto<string>> EliminarAsync(int id);
        Task<ApiResponseDto<DatoConstanteDto?>> ObtenerPorIdAsync(int id);
        Task<ApiResponseDto<DatoConstanteDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponseDto<List<DatoConstanteDto>?>> ListarAsync();
    }
}
