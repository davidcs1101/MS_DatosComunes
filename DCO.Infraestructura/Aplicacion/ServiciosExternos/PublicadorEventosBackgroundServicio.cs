using System.Net.Http.Json;
using DCO.Dtos;
using Utilidades;
using DCO.Infraestructura.Servicios.Interfaces;
using DCO.Aplicacion.ServiciosExternos;

namespace DCO.Infraestructura.Aplicacion.ServiciosExternos
{
    public class PublicadorEventosBackgroundServicio: IPublicadorEventosBackgroundServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;

        public PublicadorEventosBackgroundServicio(HttpClient httpClient, IRespuestaHttpValidador respuestaHttpValidador)
        {
            _httpClient = httpClient;
            _respuestaHttpValidador = respuestaHttpValidador;
        }

        public async Task<HttpResponseMessage> PublicarActualizacionListaDetalle(string urlNotificar, List<ListaDetalleDto> listaDetalleRequest)
        {
            var respuesta = await _httpClient.PostAsJsonAsync(urlNotificar, listaDetalleRequest);

            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }
    }
}
