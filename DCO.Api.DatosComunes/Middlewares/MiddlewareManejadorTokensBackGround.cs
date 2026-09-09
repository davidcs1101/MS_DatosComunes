using System.Net.Http.Headers;
using Microsoft.Extensions.Caching.Memory;
using DCO.Aplicacion.ServiciosExternos.config;
using DCO.Dominio.Excepciones;
using Utilidades.Servicios.Http.Interfaces;
using Utilidades.Dtos.Seguridad;

namespace DCO.Api.DatosComunes.Middlewares
{
    public class MiddlewareManejadorTokensBackground : DelegatingHandler
    {
        private readonly IMemoryCache _cache;
        private readonly IMSSeguridadAutenticacion _seguridadAutenticacion;
        private readonly IAppSettings _appSettings;

        public MiddlewareManejadorTokensBackground(IMSSeguridadAutenticacion msSeguridadAutenticacion, IAppSettings appSettings, IMemoryCache cache)
        {
            _seguridadAutenticacion = msSeguridadAutenticacion;
            _appSettings = appSettings;
            _cache = cache;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Intenta obtener el token desde caché
            if (!_cache.TryGetValue("Token", out string token))
            {
                var datosToken = await AutenticarUsuarioAsync();
                token = datosToken.Token;

                //Calculamos el tiempo hasta la expiración
                var ahora = DateTime.UtcNow;
                var expiracion = datosToken.FechaExpiracion.ToUniversalTime();
                var duracion = expiracion - ahora;

                //Si por alguna razón la diferencia es negativa (ej. reloj del servidor), aplicamos un mínimo
                if (duracion <= TimeSpan.Zero)
                    duracion = TimeSpan.FromMinutes(1);

                //Restamos unos minutos de margen antes de que expire (por seguridad)
                var duracionConMargen = duracion - TimeSpan.FromMinutes(1);
                if (duracionConMargen < TimeSpan.Zero)
                    duracionConMargen = TimeSpan.FromMinutes(1);

                //Guardamos el token en memoria segun la duración calculada con base en la fecha de expiración obtenida desde el servicio.
                _cache.Set("Token", token, duracionConMargen);
            }

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Dejamos el método de autenticación acá y no a nivel de una interfaz publica 
        /// de servicio de la capa de aplicación para evitar que se haga loguin desde
        /// alguna otra parte de la aplicación. únicamente el Middleware es quien
        /// contralará que se haga consulta de token de usuario, y tal y como se observa 
        /// en el método sólo se hace con el usuario de integración para contacto entre
        /// Microservicios.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="LoguinException"></exception>
        private async Task<AutenticacionResponse> AutenticarUsuarioAsync()
        {
            var trabajosColasSettings = _appSettings.ObtenerTrabajosColasSettings();
            AutenticacionRequest autenticacionRequest = new AutenticacionRequest()
            {
                NombreUsuario = trabajosColasSettings.UsuarioIntegracion,
                Clave = trabajosColasSettings.ClaveIntegracion
            };
            return await _seguridadAutenticacion.AutenticarUsuarioAsync(autenticacionRequest);
        }
    }
}
