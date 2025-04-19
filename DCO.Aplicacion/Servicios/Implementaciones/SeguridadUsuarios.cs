using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Aplicacion.ServiciosExternos;
using DCO.Dtos;
using System.Net.Http.Json;

namespace DCO.Aplicacion.Servicios.Implementaciones
{
    public class SeguridadUsuarios : ISeguridadUsuarios
    {
        private readonly IMSSeguridadServicio _msSeguridadServicio;

        public SeguridadUsuarios(IMSSeguridadServicio msSeguridadServicio)
        {
            _msSeguridadServicio = msSeguridadServicio;
        }

        public async Task<ApiResponse<List<UsuarioDto>?>> Listar(IdsListadoDto idsListadoDto) {

            // Consulta en lote al microservicio de seguridad
            var respuesta = await _msSeguridadServicio.ObtenerNombresUsuariosPorIds(idsListadoDto);
            var nombresUsuarios = await respuesta.Content.ReadFromJsonAsync<ApiResponse<List<UsuarioDto>?>>();
            if (nombresUsuarios is not null && !nombresUsuarios.Correcto)
                throw new KeyNotFoundException("OJO CAMBIAR: NO FUE POSIBLE OBTENER LOS DATOS DEL MICROSERVICIO DE USUARIOS");

            return nombresUsuarios;
        }
    }
}
