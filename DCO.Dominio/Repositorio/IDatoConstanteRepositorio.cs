using DCO.Dominio.Entidades;

namespace DCO.Dominio.Repositorio
{
    public interface IDatoConstanteRepositorio
    {
        void MarcarCrear(DCO_DatoConstante datoConstante);
        void MarcarModificar(DCO_DatoConstante datoConstante);
        void MarcarEliminar(DCO_DatoConstante datoConstante);
        Task<DCO_DatoConstante?> ObtenerPorIdAsync(int id);
        Task<DCO_DatoConstante?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<DCO_DatoConstante> Listar();
    }
}
