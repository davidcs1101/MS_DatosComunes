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
        IQueryable<ListaDetalleMV> Listar();
        IQueryable<ListaDetalleMV> ListarPorCodigoConstante(string codigoDatoConstante);
    }
}
