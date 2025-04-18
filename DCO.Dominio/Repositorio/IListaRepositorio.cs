using DCO.Dominio.Entidades;

namespace DCO.Dominio.Repositorio
{
    public interface IListaRepositorio
    {
        Task<int> CrearAsync(DCO_Lista lista);
        Task ModificarAsync(DCO_Lista lista);
        Task<bool> EliminarAsync(int id);
        Task<DCO_Lista?> ObtenerPorIdAsync(int id);
        Task<DCO_Lista?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<DCO_Lista> Listar();
    }
}
