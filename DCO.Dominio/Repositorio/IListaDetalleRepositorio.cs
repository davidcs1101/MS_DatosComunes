using DCO.Dominio.Entidades;
using DCO.Dominio.Entidades.ModelosVistas;

namespace DCO.Dominio.Repositorio
{
    public interface IListaDetalleRepositorio
    {
        void MarcarCrear(DCO_ListaDetalle listaDetalle);
        void MarcarModificar(DCO_ListaDetalle listaDetalle);
        void MarcarEliminar(DCO_ListaDetalle listaDetalle);
        Task<DCO_ListaDetalle?> ObtenerPorIdAsync(int id);
        Task<DCO_ListaDetalle?> ObtenerPorListaIdYCodigoAsync(int listaId,string codigo);
        Task<List<ListaDetalleMV>> ListarAsync();
        Task<List<ListaDetalleMV>> ListarPorCodigoListaAsync(string codigoLista);
        Task<List<ListaDetalleMV>> ListarPorCodigoConstanteAsync(string codigoDatoConstante);
        Task<List<ListaDetalleMV>> ListarPorCodigosListaAsync(List<string> codigosLista);
        Task<List<ListaDetalleMV>> ListarPorCodigosConstanteAsync(List<string> codigosConstante);
    }
}
