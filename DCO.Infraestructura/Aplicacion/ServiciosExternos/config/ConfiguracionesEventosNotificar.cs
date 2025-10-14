using Microsoft.Extensions.Options;
using DCO.Aplicacion.ServiciosExternos.config;
using DCO.Dtos.AppSettings;
using System.Security.Policy;
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
            return ObtenerListasUrls(urls);
        }

        public List<string?> ObtenerActualizarConstantesDetalleServicios()
        {
            var urls = _opciones.ActualizarConstantesDetalleServicios;
            return ObtenerListasUrls(urls);
        }

        private List<string?> ObtenerListasUrls(List<string?> urls) 
        {
            var urlsCompletas = new List<string?>();
            foreach (var url in urls)
                urlsCompletas.Add(url);

            return urlsCompletas ?? new List<string?>();
        }
    }
}
