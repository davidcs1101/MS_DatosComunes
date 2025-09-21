using Microsoft.EntityFrameworkCore;
//using MySqlConnector;
using Newtonsoft.Json;
using System.Net;
using Utilidades;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Dominio.Excepciones;

namespace DCO.Api.DatosComunes.Middlewares
{
    public class MiddlewareExcepcionesGlobales
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly IServiceProvider _serviceProvider;
        public MiddlewareExcepcionesGlobales(RequestDelegate requestDelegate, IServiceProvider serviceProvider)
        {
            _requestDelegate = requestDelegate;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext httpContext) 
        {
            try
            {
                //Llamamos al siguiente MiddleWare en la cadena de ejecución
                await _requestDelegate(httpContext);
            }
            catch (Exception e)
            {
                await ManejarExcepcionesAsync(httpContext, e);
            }
        }

        private Task ManejarExcepcionesAsync(HttpContext contexto, Exception e) 
        {
            contexto.Response.ContentType = "application/json";
            using (var scope = _serviceProvider.CreateScope()) 
            {
                var _apiResponse = scope.ServiceProvider.GetRequiredService<IApisResponse>();
                var respuesta = _apiResponse.CrearRespuesta(false, Textos.Generales.MENSAJE_ERROR_SERVIDOR, "");

                if (e is DatoNoEncontradoException)
                {
                    contexto.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    respuesta.Mensaje = e.Message;
                }
                else if (e is DatoYaExisteException)
                {
                    contexto.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    respuesta.Mensaje = e.Message;
                }
                else if (e is SolicitudHttpException)
                {
                    contexto.Response.StatusCode = (int)HttpStatusCode.BadGateway;
                    respuesta.Mensaje = e.Message;
                }
                else
                {
                    contexto.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                }

                //Siempre escribimos en los logs las diferentes Excepciones
                Logs.EscribirLog("e", "", e);

                // Si es desarrollo, incluir el detalle del error
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    //respuesta.Mensaje = e.Message;
                }

                var respuestaJson = JsonConvert.SerializeObject(respuesta);
                return contexto.Response.WriteAsync(respuestaJson);
            }
        }
    }
}
