using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Aplicacion.Servicios.Interfaces.Cache;
using DCO.Aplicacion.ServiciosExternos;
namespace DCO.Aplicacion.Servicios.Implementaciones
{
    public class SincronizadorMicroservicios : ISincronizadorMicroservicios
    {
        /// <inheritdoc/>
        private readonly IJobEncoladorServicio _jobEncoladorServicio;

        public SincronizadorMicroservicios(IJobEncoladorServicio jobEncoladorServicio)
        {
            _jobEncoladorServicio = jobEncoladorServicio;
        }

        public async Task SincronizarTareasAsync(List<int> colasSolicitudIds)
        {
            _ = _jobEncoladorServicio.EncolarPorColasSolicitudesIds(colasSolicitudIds, true);
        }

        public async Task SincronizarTareaAsync(int colaSolicitudId)
        {
            _ = _jobEncoladorServicio.EncolarPorColaSolicitudId(colaSolicitudId, true);
        }
    }
}
