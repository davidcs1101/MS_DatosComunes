using DCO.Dtos;
using Utilidades.Dtos;

namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IListaServicio
    {
        Task<ApiResponseDto<int>> CrearAsync(ListaCreacionRequest listaCreacionRequest);
        Task<ApiResponseDto<string>> ModificarAsync(ListaModificacionRequest listaModificacionRequest);
        Task<ApiResponseDto<string>> EliminarAsync(int id);
        Task<ApiResponseDto<ListaDto?>> ObtenerPorIdAsync(int id);
        Task<ApiResponseDto<ListaDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponseDto<List<ListaDto>?>> ListarAsync();
    }
}
