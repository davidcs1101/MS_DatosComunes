using System.Net.Http.Json;
using DCO.Dtos;
using DCO.Aplicacion.ServiciosExternos;
using Utilidades;
using DCO.Infraestructura.Servicios.Interfaces;

namespace DCO.Infraestructura.Aplicacion.ServiciosExternos
{
    public class MSSeguridadServicio : IMSSeguridadServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;

        public MSSeguridadServicio(HttpClient httpClient, IRespuestaHttpValidador respuestaHttpValidador)
        {
            _httpClient = httpClient;
            _respuestaHttpValidador = respuestaHttpValidador;
        }

        public async Task<HttpResponseMessage> ObtenerNombreUsuarioPorIdAsync(int id)
        {
            var url = $"api/usuarios/obtenerNombreUsuarioPorId?id={id}";
            var respuesta = await _httpClient.GetAsync(url);

            _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }

        public async Task<HttpResponseMessage> ObtenerNombresUsuariosPorIds(IdsListadoDto usuarioIds) 
        {
            var url = "api/usuarios/listar";
            var respuesta = await _httpClient.PostAsJsonAsync(url, usuarioIds);

            _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }
    }
}
