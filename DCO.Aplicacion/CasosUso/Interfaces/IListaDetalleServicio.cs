using DCO.Dominio.Entidades.ModelosVistas;
using DCO.Dtos;

namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IListaDetalleServicio
    {
        Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista);
        Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoConstanteAsync(string codigoConstante);
        Task<ApiResponse<ListaDetalleDto?>> ObtenerPorCodigoListaYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest);
        Task<ApiResponse<ListaDetalleDto?>> ObtenerPorCodigoConstanteYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest);
    }
}
