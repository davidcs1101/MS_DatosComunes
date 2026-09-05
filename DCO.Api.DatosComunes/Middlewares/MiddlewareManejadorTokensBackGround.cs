using System.Net.Http.Headers;
using DCO.Aplicacion.ServiciosExternos.config;
using DCO.Dominio.Excepciones;
using Utilidades.Servicios.Http.Interfaces;
using Utilidades.Dtos.Seguridad;

namespace DCO.Api.DatosComunes.Middlewares
{
    public class MiddlewareManejadorTokensBackground : DelegatingHandler
    {
        private readonly IMSSeguridadAutenticacion _seguridadAutenticacion;
        private readonly IAppSettings _appSettings;

        public MiddlewareManejadorTokensBackground(IMSSeguridadAutenticacion msSeguridadAutenticacion, IAppSettings appSettings)
        {
            _seguridadAutenticacion = msSeguridadAutenticacion;
            _appSettings = appSettings;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var datosToken = await AutenticarUsuarioAsync();
            var token = datosToken.Token;
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
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
