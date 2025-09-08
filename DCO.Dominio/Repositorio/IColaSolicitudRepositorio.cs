using DCO.Dominio.Entidades;

namespace DCO.Dominio.Repositorio
{
    public interface IColaSolicitudRepositorio
    {
        void MarcarCrear(DCO_ColaSolicitud colaSolicitud);
        void MarcarModificar(DCO_ColaSolicitud colaSolicitud);
        Task<DCO_ColaSolicitud?> ObtenerPorIdAsync(int id);
        IQueryable<DCO_ColaSolicitud> Listar();
    }
}
