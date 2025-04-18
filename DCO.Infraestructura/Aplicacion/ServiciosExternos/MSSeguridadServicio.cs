using System.Net.Http.Json;
using DCO.Dtos;
using DCO.Aplicacion.ServiciosExternos;
using Utilidades;

namespace DCO.Infraestructura.Aplicacion.ServiciosExternos
{
    public class MSSeguridadServicio : IMSSeguridadServicio
    {
        private readonly HttpClient _httpClient;
        public MSSeguridadServicio(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> ObtenerNombreUsuarioPorIdAsync(int id)
        {
            var url = $"api/usuarios/obtenerNombreUsuarioPorId?id={id}";
            var respuesta = await _httpClient.GetAsync(url);

            if (!respuesta.IsSuccessStatusCode)
                throw new HttpRequestException($"{Textos.Generales.MENSAJE_CORREO_ENVIADO_ERROR}: No se pudo obtener el nombre de usuario. : {respuesta.ReasonPhrase}");

            return respuesta;
        }
        //ApiResponse<List<UsuarioDto>?>
        public async Task<HttpResponseMessage> ObtenerNombresUsuariosPorIds(List<int?>? usuarioIds) 
        {
            var url = "api/usuarios/listar";
            var respuesta = await _httpClient.PostAsJsonAsync(url, new IdsListadoDto { Ids = usuarioIds });

            if (!respuesta.IsSuccessStatusCode)
                throw new HttpRequestException($"{Textos.Generales.MENSAJE_CORREO_ENVIADO_ERROR}: No se pudo obtener los nombres de los usuarios. : {respuesta.ReasonPhrase}");

            return respuesta;
        }
    }
}
