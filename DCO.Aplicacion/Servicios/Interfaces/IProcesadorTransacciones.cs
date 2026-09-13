using DCO.Dominio.Entidades;
using Utilidades.Dtos;

namespace DCO.Aplicacion.Servicios.Interfaces
{
    public interface IProcesadorTransacciones
    {
        Task EjecutarEnTransaccionAsync(Func<Task> operacion);
    }
}
