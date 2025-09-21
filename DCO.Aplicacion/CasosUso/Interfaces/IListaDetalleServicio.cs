using DCO.Dominio.Entidades.ModelosVistas;
using DCO.Dtos;

namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IListaDetalleServicio
    {
        Task<ApiResponse<int>> CrearAsync(ListaDetalleCreacionRequest listaDetalleCreacionRequest);
        Task<ApiResponse<string>> ModificarAsync(ListaDetalleModificacionRequest listaDetalleModificacionRequest);
        Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista);
        Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoConstanteAsync(string codigoConstante);
        Task<ApiResponse<ListaDetalleDto?>> ObtenerPorCodigoListaYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest);
        Task<ApiResponse<ListaDetalleDto?>> ObtenerPorCodigoConstanteYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest);
    }
}
