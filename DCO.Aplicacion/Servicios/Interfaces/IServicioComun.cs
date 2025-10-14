using DCO.Dominio.Entidades;
using DCO.Dtos;

namespace DCO.Aplicacion.Servicios.Interfaces
{
    public interface IServicioComun
    {
        Task EjecutarEnTransaccionAsync(Func<Task> operacion);
        void EncolarSolicitudes(List<DCO_ColaSolicitud> listaColasSolicitudes);
        Task<List<ListaDetalleDto>?> ObtenerListasDetallePorCodigoListaAsync(string codigoLista);
        Task<List<ListaDetalleDto>?> ObtenerListasDetalleCodigoConstanteAsync(string codigoConstante);
    }
}
