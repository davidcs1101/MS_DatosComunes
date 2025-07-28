using DCO.Dtos;

namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IDatoConstanteDetalleServicio
    {
        Task<ApiResponse<DatoConstanteDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponse<List<DatoConstanteDto>?>> ListarAsync();
    }
}
