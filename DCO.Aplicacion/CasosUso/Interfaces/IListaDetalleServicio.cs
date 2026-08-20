using DCO.Dtos;
using Utilidades.Dtos;

namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IListaDetalleServicio
    {
        Task<ApiResponseDto<int>> CrearAsync(ListaDetalleCreacionRequest listaDetalleCreacionRequest);
        Task<ApiResponseDto<string>> ModificarAsync(ListaDetalleModificacionRequest listaDetalleModificacionRequest);
        Task<ApiResponseDto<string>> EliminarAsync(int id);
        Task<ApiResponseDto<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista);
        Task<ApiResponseDto<List<ListaDetalleDto>?>> ListarPorCodigoConstanteAsync(string codigoConstante);
        Task<ApiResponseDto<ListaDetalleDto?>> ObtenerPorCodigoListaYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest);
        Task<ApiResponseDto<ListaDetalleDto?>> ObtenerPorCodigoConstanteYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest);

        Task<ApiResponseDto<List<ListaDetalleDto>?>> ListarPorCodigosListaAsync(List<string> codigosLista);
        Task<ApiResponseDto<List<ListaDetalleDto>?>> ListarPorCodigosConstanteAsync(List<string> codigosConstante);
    }
}
