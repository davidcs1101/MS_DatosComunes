using DCO.Aplicacion.ServiciosExternos;
using DCO.Dominio.Repositorio.UnidadTrabajo;
using DCO.Dominio.Excepciones;
using DCO.Dominio.Entidades;

namespace DCO.Aplicacion.Servicios.Interfaces
{
    public class ServicioComun : IServicioComun
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IJobEncoladorServicio _jobEncoladorServicio;

        public ServicioComun(IUnidadDeTrabajo unidadDeTrabajo, IJobEncoladorServicio jobEncoladorServicio)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _jobEncoladorServicio = jobEncoladorServicio;
        }

        public async Task EjecutarEnTransaccionAsync(Func<Task> operacion)
        {
            await using var transaccion = await _unidadDeTrabajo.IniciarTransaccionAsync();

            try{
                await operacion();
                await transaccion.CommitAsync();
            }
            catch (DatoNoEncontradoException){
                await transaccion.RollbackAsync();
                throw;
            }
            catch (DatoYaExisteException){
                await transaccion.RollbackAsync();
                throw;}
            catch{
                await transaccion.RollbackAsync();
                throw;
            }
        }

        public void EncolarSolicitudes(List<DCO_ColaSolicitud> listaColasSolicitudes) 
        {
            foreach (var cola in listaColasSolicitudes)
                _ = _jobEncoladorServicio.EncolarPorColaSolicitudId(cola.Id, true);
        }

    }
}
