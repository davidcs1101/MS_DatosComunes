using DCO.Dtos;
using DCO.Servicio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Utilidades;

namespace DCO.Servicio.Implementaciones
{
    public class MSSeguridadServicio : IMSSeguridadServicio
    {
        private readonly HttpClient _httpClient;
        public MSSeguridadServicio(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<ApiResponse<string>> ObtenerNombreUsuarioPorIdAsync(int id)
        {
            var url = $"api/usuarios/obtenerNombreUsuarioPorId?id={id}";
            var nombreUsuario = await _httpClient.GetFromJsonAsync<ApiResponse<string>>(url);

            if (nombreUsuario == null)
                throw new HttpRequestException($"{Textos.Generales.MENSAJE_CORREO_ENVIADO_ERROR}: No se pudo obtener el nombre de usuario.");

            return nombreUsuario;
        }

        public async Task<ApiResponse<List<UsuarioDto>?>> ObtenerNombresUsuariosPorIds(List<int?>? usuarioIds) 
        {
            var url = "api/usuarios/listar";
            var respuesta = await _httpClient.PostAsJsonAsync(url, new IdsListadoDto { Ids = usuarioIds });

            if (!respuesta.IsSuccessStatusCode)
                throw new HttpRequestException($"{Textos.Generales.MENSAJE_CORREO_ENVIADO_ERROR}: No se pudo obtener los nombres de los usuarios.");

            return await respuesta.Content.ReadFromJsonAsync<ApiResponse<List<UsuarioDto>?>>();
        }
    }
}
