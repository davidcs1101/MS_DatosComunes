using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Aplicacion.ServiciosExternos;
using DCO.Dtos;
using Utilidades;

namespace DCO.Aplicacion.Servicios.Implementaciones
{
    public class MSSeguridad : IMSSeguridad
    {
        private readonly IMSSeguridadContextoWebServicio _msSeguridadContextoWebServicio;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;

        public MSSeguridad(IMSSeguridadContextoWebServicio msSeguridadContextoWebServicio, ISerializadorJsonServicio serializadorJsonServicio)
        {
            _msSeguridadContextoWebServicio = msSeguridadContextoWebServicio;
            _serializadorJsonServicio = serializadorJsonServicio;
        }

        public async Task<List<UsuarioDto>?> ListarUsuarios(IdsListadoDto idsListadoDto) 
        {
            var respuesta = await _msSeguridadContextoWebServicio.ObtenerNombresUsuariosPorIds(idsListadoDto);
            var contenidoJson = await respuesta.Content.ReadAsStringAsync();
            var resultado = _serializadorJsonServicio.Deserializar<ApiResponse<List<UsuarioDto>?>>(contenidoJson);
            if (resultado is null || !resultado.Correcto) {
                Logs.EscribirLog("e", "OJO CAMBIAR: NO FUE POSIBLE OBTENER LOS DATOS DEL MICROSERVICIO DE USUARIOS");
                return new List<UsuarioDto>();
            }

            return resultado.Data;
        }
    }
}
