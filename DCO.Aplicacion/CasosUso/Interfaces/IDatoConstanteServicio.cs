using DCO.Dtos;

namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IDatoConstanteServicio
    {
        Task<ApiResponse<int>> CrearAsync(DatoConstanteCreacionRequest datoConstanteCreacionRequest);
        Task<ApiResponse<string>> ModificarAsync(DatoConstanteModificacionRequest datoConstanteModificacionRequest);
        Task<ApiResponse<string>> EliminarAsync(int id);
        Task<ApiResponse<DatoConstanteDto?>> ObtenerPorIdAsync(int id);
        Task<ApiResponse<DatoConstanteDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponse<List<DatoConstanteDto>?>> ListarAsync();
    }
}
