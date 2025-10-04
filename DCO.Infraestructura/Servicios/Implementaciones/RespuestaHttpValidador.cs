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
                var error = new ApiResponse<string>();
                detalleError = $"{mensaje} {respuesta.ReasonPhrase}. ";
                try{
                    error = await respuesta.Content.ReadFromJsonAsync<ApiResponse<string>>();
                    if (error is not null && !string.IsNullOrWhiteSpace(error.Mensaje))
                        detalleError += $"{error.Mensaje}. ";
                }
                catch (Exception e){
                    detalleError += e.Message;
                }
                throw new SolicitudHttpException(detalleError);
            }
        }
    }
}
