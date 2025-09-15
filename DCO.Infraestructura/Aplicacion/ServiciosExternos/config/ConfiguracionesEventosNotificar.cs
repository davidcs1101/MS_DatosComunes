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
            return _opciones.ActualizarListasDetalleServicios ?? new List<string?>();
        }
    }
}
