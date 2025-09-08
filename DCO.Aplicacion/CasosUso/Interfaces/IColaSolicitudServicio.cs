namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IColaSolicitudServicio
    {
        Task ProcesarColaSolicitudesAsync();
        Task ProcesarPorColaSolicitudIdAsync(int id, bool validarEstadoPendiente = false);
    }
}
