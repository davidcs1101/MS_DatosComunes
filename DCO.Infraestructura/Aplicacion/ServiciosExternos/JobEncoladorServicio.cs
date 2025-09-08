using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.ServiciosExternos;
using Utilidades;
using Hangfire;

public class JobEncoladorServicio: IJobEncoladorServicio
{
    public Task EncolarPorColaSolicitudId(int Id, bool validarEstadoPendiente = false)
    {
        try{
            BackgroundJob.Enqueue<IColaSolicitudServicio>(x => x.ProcesarPorColaSolicitudIdAsync(Id, validarEstadoPendiente));
        }
        catch (Exception e){
            Logs.EscribirLog("e", Textos.ColasSolicitudes.MENSAJE_COLASOLICITUD_ERROR_ENCOLAR_HANGFIRE, e);
        }
        return Task.CompletedTask;
    }
}