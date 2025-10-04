using Microsoft.Extensions.Options;
using DCO.Aplicacion.ServiciosExternos.config;
using DCO.Dtos.AppSettings;
namespace DCO.Infraestructura.Aplicacion.ServiciosExternos.Config
{
    public class ConfiguracionesEventosNotificar: IConfiguracionesEventosNotificar
    {
        private readonly EventosNotificarSettings _opciones;

        public ConfiguracionesEventosNotificar(IOptions<EventosNotificarSettings> opciones)
        {
            _opciones = opciones.Value;
        }

        public List<string?> ObtenerActualizarListasDetalleServicios()
        {
            var urls = _opciones.ActualizarListasDetalleServicios;
            var urlsCompletas = new List<string?>();
            foreach (var url in urls)
                urlsCompletas.Add(url);

            return urlsCompletas ?? new List<string?>();
        }
    }
}
