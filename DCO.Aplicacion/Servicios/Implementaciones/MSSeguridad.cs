using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Aplicacion.ServiciosExternos;
using DCO.Dtos;

namespace DCO.Aplicacion.Servicios.Implementaciones
{
    public class MSSeguridad : IMSSeguridad
    {
        private readonly IMSSeguridadContextoWebServicio _msSeguridadServicio;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;

        public MSSeguridad(IMSSeguridadContextoWebServicio msSeguridadServicio, ISerializadorJsonServicio serializadorJsonServicio)
        {
            _msSeguridadServicio = msSeguridadServicio;
            _serializadorJsonServicio = serializadorJsonServicio;
        }

        public async Task<List<UsuarioDto>?> ListarUsuarios(IdsListadoDto idsListadoDto) 
        {
            var respuesta = await _msSeguridadServicio.ObtenerNombresUsuariosPorIds(idsListadoDto);
            var contenidoJson = await respuesta.Content.ReadAsStringAsync();
            var resultado = _serializadorJsonServicio.Deserializar<ApiResponse<List<UsuarioDto>?>>(contenidoJson);
            if (resultado is not null && !resultado.Correcto)
                throw new KeyNotFoundException("OJO CAMBIAR: NO FUE POSIBLE OBTENER LOS DATOS DEL MICROSERVICIO DE USUARIOS");

            return resultado.Data;
        }
    }
}
