using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using MySqlConnector;
using Newtonsoft.Json;
using DCO.Dtos;
using System;
using System.Net;
using Utilidades;

namespace DCO.Api.DatosComunes.Infraestructura
{
    public class MiddlewareExcepcionesGlobales
    {
        private readonly RequestDelegate _requestDelegate;

        public MiddlewareExcepcionesGlobales(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
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
            var respuesta = new ApiResponse<string>
            {
                Correcto = false,
                Mensaje = Textos.Generales.MENSAJE_ERROR_SERVIDOR
            };

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
