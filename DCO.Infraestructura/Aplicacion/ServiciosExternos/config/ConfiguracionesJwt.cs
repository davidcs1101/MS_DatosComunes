using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using DCO.Aplicacion.ServiciosExternos;
using DCO.Aplicacion.ServiciosExternos.config;
using DCO.Dtos.AppSettings;
namespace DCO.Infraestructura.Aplicacion.ServiciosExternos.Config
{
    public class ConfiguracionesJwt: IConfiguracionesJwt
    {
        private readonly JWTSettings _opciones;
        private readonly IServiceProvider _serviceProvider;

        public ConfiguracionesJwt(IOptions<JWTSettings> opciones, IServiceProvider serviceProvider)
        {
            _opciones = opciones.Value;
            _serviceProvider = serviceProvider;
        }

        public string ObtenerEmisor()
        {
            return string.IsNullOrWhiteSpace(_opciones.Emisor) ? "" : _opciones.Emisor;
        }

        public string ObtenerAudienciasDestinoTexto()
        {
            var audienciasDestinotexto = "";
            using (var scope = _serviceProvider.CreateScope()) 
            {
                var _serializadorJson = scope.ServiceProvider.GetRequiredService<ISerializadorJsonServicio>();
                audienciasDestinotexto = _serializadorJson.Serializar(_opciones.AudienciasDestino);
            }
            return string.IsNullOrWhiteSpace(audienciasDestinotexto) ? "" : audienciasDestinotexto;
        }

        public List<string?> ObtenerAudienciasDestino()
        {
            return _opciones.AudienciasDestino ?? new List<string?>();
        }

        public string ObtenerLlave()
        {
            return string.IsNullOrWhiteSpace(_opciones.Llave) ? "" : _opciones.Llave;
        }

        public int ObtenerMinutosDuracionTokenAutenticacionUsuario()
        {
            if (int.TryParse(_opciones.MinutosDuracionTokenAutenticacionUsuario, out int dato))
                return dato;
            return 0;
        }

        public int ObtenerMinutosDuracionTokenAutenticacionSede()
        {
            if (int.TryParse(_opciones.MinutosDuracionTokenAutenticacionSede, out int dato))
                return dato;
            return 0;
        }
    }
}
