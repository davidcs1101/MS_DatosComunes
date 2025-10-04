using DCO.Dominio.Entidades;

namespace DCO.Aplicacion.Servicios.Interfaces
{
    public interface IServicioComun
    {
        Task EjecutarEnTransaccionAsync(Func<Task> operacion);
        void EncolarSolicitudes(List<DCO_ColaSolicitud> listaColasSolicitudes);
    }
}
