namespace DCO.Aplicacion.ServiciosExternos
{
    public interface IJobEncoladorServicio
    {
        Task EncolarPorColaSolicitudId(int id, bool validarEstadoPendiente = false);
        Task EncolarPorColasSolicitudesIds(List<int> ids, bool validarEstadoPendiente = false);
    }
}
