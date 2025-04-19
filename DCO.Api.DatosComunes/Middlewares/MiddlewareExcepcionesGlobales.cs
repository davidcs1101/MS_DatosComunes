using Microsoft.EntityFrameworkCore;
//using MySqlConnector;
using Newtonsoft.Json;
using DCO.Dtos;
using System.Net;
using Utilidades;
using DCO.Aplicacion.Servicios.Interfaces;

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

        private Task ManejarExcepcionesAsync(HttpContext httpContext, Exception e) 
        {
            httpContext.Response.ContentType = "application/json";
            using (var scope = _serviceProvider.CreateScope()) 
            {
                var _apiResponse = scope.ServiceProvider.GetRequiredService<IApiResponse>();
                var respuesta = _apiResponse.CrearRespuesta(false, Textos.Generales.MENSAJE_ERROR_SERVIDOR, "");

                if (e is KeyNotFoundException)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    respuesta.Mensaje = e.Message;
                }
                else if (e is DbUpdateException)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    respuesta.Mensaje = e.Message;
                }
                else
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    Logs.EscribirLog("e", "", e);
                }

                // Si es desarrollo, incluir el detalle del error
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    //respuesta.Mensaje = e.Message;
                }

                var respuestaJson = JsonConvert.SerializeObject(respuesta);
                return httpContext.Response.WriteAsync(respuestaJson);
            }
        }
    }
}
