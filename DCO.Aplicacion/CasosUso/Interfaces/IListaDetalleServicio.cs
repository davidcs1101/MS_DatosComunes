using DCO.Dtos;

namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IListaDetalleServicio
    {
        Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista);
        Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoConstanteAsync(string codigoConstante);
        Task<ApiResponse<bool>> ValidarIdDetalleExisteEnCodigoListaAsync(CodigoListaIdDetalleRequest codigoListaIdDetalleRequest);
    }
}
