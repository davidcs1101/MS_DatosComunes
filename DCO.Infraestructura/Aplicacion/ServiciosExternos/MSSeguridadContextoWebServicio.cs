using System.Net.Http.Json;
using DCO.Dtos;
using DCO.Aplicacion.ServiciosExternos;
using Utilidades;
using DCO.Infraestructura.Servicios.Interfaces;

namespace DCO.Infraestructura.Aplicacion.ServiciosExternos
{
    public class MSSeguridadContextoWebServicio : IMSSeguridadContextoWebServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;

        public MSSeguridadContextoWebServicio(HttpClient httpClient, IRespuestaHttpValidador respuestaHttpValidador)
        {
            _httpClient = httpClient;
            _respuestaHttpValidador = respuestaHttpValidador;
        }

        public async Task<HttpResponseMessage> ObtenerNombreUsuarioPorIdAsync(int id)
        {
            var url = $"seg/usuarios/obtenerNombreUsuarioPorId?id={id}";
            var respuesta = await _httpClient.GetAsync(url);

            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }

        public async Task<HttpResponseMessage> ObtenerNombresUsuariosPorIds(IdsListadoDto usuarioIds) 
        {
            var url = "seg/usuarios/listar";
            var respuesta = await _httpClient.PostAsJsonAsync(url, usuarioIds);

            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);

            return respuesta;
        }
    }
}
