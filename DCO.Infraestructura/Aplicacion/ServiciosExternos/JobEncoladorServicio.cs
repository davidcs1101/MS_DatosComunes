using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.ServiciosExternos;
using Utilidades;
using Hangfire;

public class JobEncoladorServicio: IJobEncoladorServicio
{
    public Task EncolarPorColaSolicitudId(int id, bool validarEstadoPendiente = false)
    {
        EncolarSolicitudId(id, validarEstadoPendiente);
        return Task.CompletedTask;
    }

    public Task EncolarPorColasSolicitudesIds(List<int> ids, bool validarEstadoPendiente = false)
    {
        foreach (var id in ids)
        {
            EncolarSolicitudId(id, validarEstadoPendiente);
        }
        return Task.CompletedTask;
    }

    private void EncolarSolicitudId(int id, bool validarEstadoPendiente = false)
    {
        try
        {
            BackgroundJob.Enqueue<IColaSolicitudServicio>(x => x.ProcesarPorColaSolicitudIdAsync(id, validarEstadoPendiente));
        }
        catch (Exception e)
        {
            Logs.EscribirLog("e", Textos.ColasSolicitudes.MENSAJE_COLASOLICITUD_ERROR_ENCOLAR_HANGFIRE, e);
        }
    }
}