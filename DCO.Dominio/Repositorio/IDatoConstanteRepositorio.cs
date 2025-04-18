using DCO.Dominio.Entidades;

namespace DCO.Dominio.Repositorio
{
    public interface IDatoConstanteRepositorio
    {
        Task<int> CrearAsync(DCO_DatoConstante DatoConstante);
        Task ModificarAsync(DCO_DatoConstante DatoConstante);
        Task<bool> EliminarAsync(int id);
        Task<DCO_DatoConstante?> ObtenerPorIdAsync(int id);
        Task<DCO_DatoConstante?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<DCO_DatoConstante> Listar();
    }
}
