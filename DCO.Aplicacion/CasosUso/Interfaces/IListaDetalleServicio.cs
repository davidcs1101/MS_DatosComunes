using DCO.Dominio.Entidades.ModelosVistas;
using DCO.Dtos;

namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IListaDetalleServicio
    {
        Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista);
        Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoConstanteAsync(string codigoConstante);
        Task<ApiResponse<string>> ValidarIdDetalleExisteEnCodigoListaAsync(CodigoIdListaDetalleRequest codigoListaIdDetalleRequest);
        Task<ApiResponse<string>> ValidarIdDetalleExisteEnCodigoConstanteAsync(CodigoIdListaDetalleRequest codigoListaIdDetalleRequest);
        Task<ApiResponse<ListaDetalleDto?>> ObtenerPorCodigoConstanteYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest);
    }
}
