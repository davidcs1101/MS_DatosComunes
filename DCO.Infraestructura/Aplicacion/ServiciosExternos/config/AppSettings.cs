using Microsoft.Extensions.Options;
using DCO.Dtos.AppSettings;
using DCO.Aplicacion.ServiciosExternos.config;
namespace DCO.Infraestructura.Aplicacion.ServiciosExternos.Config
{
    public class AppSettings : IAppSettings
    {
        private readonly TrabajosColasSettings _trabajosColas;
        private readonly EventosNotificarSettings _eventosNotificar;

        public AppSettings(
            IOptions<TrabajosColasSettings> opcionesTrabajosColas, IOptions<EventosNotificarSettings> eventosNotificar)
        {
            _trabajosColas = opcionesTrabajosColas.Value;
            _eventosNotificar = eventosNotificar.Value;
        }


        //TrabajosColas
        public TrabajosColasSettings ObtenerTrabajosColasSettings() 
        {
            return new TrabajosColasSettings
            {
                CantidadIntentosPorRegistroEnCola = _trabajosColas.CantidadIntentosPorRegistroEnCola,

                CantidadRegistrosProcesarIteracion = _trabajosColas.CantidadRegistrosProcesarIteracion,

                ProcesarColaSolicitudesCron =
                    string.IsNullOrWhiteSpace(_trabajosColas.ProcesarColaSolicitudesCron)
                        ? "*/5 * * * *"
                        : _trabajosColas.ProcesarColaSolicitudesCron,

                UsuarioIntegracion = _trabajosColas.UsuarioIntegracion,

                ClaveIntegracion = _trabajosColas.ClaveIntegracion
            };
        }

        //EventosNotificar/ActualizarListasDetalleServicios
        public List<string?> ObtenerActualizarListasDetalleServicios()
        {
            var urls = _eventosNotificar.ActualizarListasDetalleServicios;
            return ObtenerListas(urls);
        }

        //EventosNotificar/ActualizarConstantesDetalleServicios
        public List<string?> ObtenerActualizarConstantesDetalleServicios()
        {
            var urls = _eventosNotificar.ActualizarConstantesDetalleServicios;
            return ObtenerListas(urls);
        }




        private List<string?> ObtenerListas(List<string?> lista)
        {
            var urlsCompletas = new List<string?>();
            foreach (var url in lista)
                urlsCompletas.Add(url);

            return urlsCompletas ?? new List<string?>();
        }
    }
}
