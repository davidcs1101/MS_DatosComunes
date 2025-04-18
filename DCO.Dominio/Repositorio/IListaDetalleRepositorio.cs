using DCO.Dominio.Entidades;
using DCO.Dominio.Entidades.ModelosVistas;

namespace DCO.Dominio.Repositorio
{
    public interface IListaDetalleRepositorio
    {
        Task<int> CrearAsync(DCO_ListaDetalle listaDetalle);
        Task ModificarAsync(DCO_ListaDetalle listaDetalle);
        Task<bool> EliminarAsync(int id);
        Task<DCO_ListaDetalle?> ObtenerPorIdAsync(int id);
        Task<DCO_ListaDetalle?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<ListaDetalleMV> Listar();
        IQueryable<ListaDetalleMV> ListarPorCodigoConstante(string codigoDatoConstante);
    }
}
