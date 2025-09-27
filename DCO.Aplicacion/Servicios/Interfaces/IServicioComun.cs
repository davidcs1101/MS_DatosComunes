namespace DCO.Aplicacion.Servicios.Interfaces
{
    public interface IServicioComun
    {
        Task EjecutarEnTransaccionAsync(Func<Task> operacion);
    }
}
