using System.Net.Http.Headers;
using DCO.Aplicacion.ServiciosExternos;
using DCO.Dtos;
using Utilidades;
using DCO.Aplicacion.ServiciosExternos.config;
using DCO.Dominio.Excepciones;

namespace DCO.Api.DatosComunes.Middlewares
{
    public class MiddlewareManejadorTokensBackGround : DelegatingHandler
    {
        private readonly IMSSeguridadBackgroundServicio _msSeguridadBackgroundServicio;
        private readonly IConfiguracionesTrabajosColas _configuracionesTrabajosColas;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;

        public MiddlewareManejadorTokensBackGround(IMSSeguridadBackgroundServicio msSeguridadBackgroundServicio, IConfiguracionesTrabajosColas configuracionesTrabajosColas, IRespuestaHttpValidador respuestaHttpValidador)
        {
            _msSeguridadBackgroundServicio = msSeguridadBackgroundServicio;
            _configuracionesTrabajosColas = configuracionesTrabajosColas;
            _respuestaHttpValidador = respuestaHttpValidador;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await this.AutenticarUsuarioAsync();
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
        private async Task<string> AutenticarUsuarioAsync()
        {
            AutenticacionRequest autenticacionRequest = new AutenticacionRequest()
            {
                NombreUsuario = _configuracionesTrabajosColas.ObtenerUsuarioIntegracion(),
                Clave = _configuracionesTrabajosColas.ObtenerClaveIntegracion()
            };

            var respuesta = await _msSeguridadBackgroundServicio.AutenticarUsuarioAsync(autenticacionRequest);
            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);
            var contenido = await respuesta.Content.ReadFromJsonAsync<ApiResponse<AutenticacionResponse>>();
            if (contenido?.Data == null)
                throw new LoguinException(Textos.Usuarios.MENSAJE_LOGIN_INCORRECTO);

            return contenido?.Data.Token;
        }
    }
}
