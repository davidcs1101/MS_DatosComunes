using DCO.Dtos;
using DCO.Aplicacion.ServiciosExternos;
using System.Net.Http.Json;
using Utilidades;
using DCO.Aplicacion.ServiciosExternos.config;

namespace DCO.Infraestructura.Aplicacion.ServiciosExternos
{
    public class PublicadorEventosBackgroundServicio : IPublicadorEventosBackgroundServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;

        public PublicadorEventosBackgroundServicio(HttpClient httpClient, IRespuestaHttpValidador respuestaHttpValidador, IConfiguracionesTrabajosColas configuracionesTrabajosColas)
        {
            _httpClient = httpClient;
            _respuestaHttpValidador = respuestaHttpValidador;
        }

        public async Task<HttpResponseMessage> PublicarActualizacionListaDetalle(string url,List<ListaDetalleDto> listaDetalleRequest) 
        {
            var requestUrl = $"{url}";
            var respuesta = await _httpClient.PostAsJsonAsync(requestUrl, listaDetalleRequest);
            await _respuestaHttpValidador.ValidarRespuesta(
                respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }
    }
}
