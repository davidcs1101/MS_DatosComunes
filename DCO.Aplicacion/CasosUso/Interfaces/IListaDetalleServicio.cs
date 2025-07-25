using DCO.Dominio.Entidades.ModelosVistas;
using DCO.Dtos;

namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IListaDetalleServicio
    {
        Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista);
        Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoConstanteAsync(string codigoConstante);
        Task<ApiResponse<string>> ValidarIdDetalleExisteEnCodigoListaAsync(CodigoListaIdDetalleRequest codigoListaIdDetalleRequest);
    }
}
