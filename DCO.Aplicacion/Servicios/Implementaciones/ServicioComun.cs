using DCO.Aplicacion.ServiciosExternos;
using DCO.Dtos;

namespace DCO.Aplicacion.Servicios.Interfaces
{
    public class ServicioComun : IServicioComun
    {
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;

        public ServicioComun(IRespuestaHttpValidador respuestaHttpValidador, ISerializadorJsonServicio serializadorJsonServicio)
        {
            _respuestaHttpValidador = respuestaHttpValidador;
            _serializadorJsonServicio = serializadorJsonServicio;
        }

        public async Task<TReturn> ObtenerIdListaDetalleAsync<TRequest, TSerializacion, TReturn>
            (Func<TRequest, Task<HttpResponseMessage>> funcionEjecutar
            , TRequest request
            , Func<TSerializacion, TReturn> obtenerValor
            )
        {
            var respuesta = await funcionEjecutar(request);
            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Utilidades.Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);
            var contenidoJson = await respuesta.Content.ReadAsStringAsync();
            var resultado = _serializadorJsonServicio.Deserializar<ApiResponse<TSerializacion?>>(contenidoJson);

            return obtenerValor(resultado.Data);
        }
    }
}
