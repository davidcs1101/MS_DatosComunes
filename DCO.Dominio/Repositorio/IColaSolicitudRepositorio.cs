using DCO.Dominio.Entidades;
using DCO.Dominio.Enumeraciones;

namespace DCO.Dominio.Repositorio
{
    public interface IColaSolicitudRepositorio
    {
        void MarcarCrear(DCO_ColaSolicitud colaSolicitud);
        void MarcarModificar(DCO_ColaSolicitud colaSolicitud);
        Task<DCO_ColaSolicitud?> ObtenerPorIdAsync(int id);
        Task<List<DCO_ColaSolicitud>> ListarAsync(EstadoCola estado, int cantidadRegistros);
        Task<int> CrearAsync(DCO_ColaSolicitud colaSolicitud);
    }
}
