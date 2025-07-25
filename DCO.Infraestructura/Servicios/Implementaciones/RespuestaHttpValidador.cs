using DCO.Dominio.Excepciones;
using DCO.Dtos;
using DCO.Infraestructura.Servicios.Interfaces;
using System.Net.Http.Json;

namespace DCO.Infraestructura.Servicios.Implementaciones
{
    public class RespuestaHttpValidador : IRespuestaHttpValidador
    {
        public async Task ValidarRespuesta(HttpResponseMessage respuesta, string mensaje)
        {
            var detalleError = "";
            if (!respuesta.IsSuccessStatusCode)
            {
                detalleError = $"{mensaje} {respuesta.ReasonPhrase}. ";
                if (respuesta.StatusCode == System.Net.HttpStatusCode.BadGateway)
                    throw new SolicitudHttpException(detalleError);
                var error = await respuesta.Content.ReadFromJsonAsync<ApiResponse<string>>();
                if (error is not null && !string.IsNullOrWhiteSpace(error.Mensaje))
                    detalleError += error.Mensaje;

                throw new SolicitudHttpException(detalleError);
            }
        }
    }
}
