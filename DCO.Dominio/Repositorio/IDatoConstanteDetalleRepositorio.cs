using DCO.Dominio.Entidades;
using DCO.Dominio.Entidades.ModelosVistas;

namespace DCO.Dominio.Repositorio
{
    public interface IDatoConstanteDetalleRepositorio
    {
        void MarcarCrear(DCO_DatoConstanteDetalle datoConstanteDetalle);
        void MarcarModificar(DCO_DatoConstanteDetalle datoConstanteDetalle);
        void MarcarEliminar(DCO_DatoConstanteDetalle datoConstanteDetalle);
        Task<DCO_DatoConstanteDetalle?> ObtenerPorId(int id); 
        Task<DCO_DatoConstanteDetalle?> ObtenerPorDatoConstanteIdYListaDetalleIdAsync(int datoConstanteId, int listaDetalleId);
    }
}
