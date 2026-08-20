using Utilidades.Dtos;
using Utilidades.Servicios.Serializacion.Interfaces;
using Utilidades.Servicios.Http.Interfaces;
using DCO.Aplicacion.Servicios.Interfaces;

namespace DCO.Aplicacion.Servicios.Implementaciones
{
    public class ServicioEjecutorHttp : IServicioEjecutorHttp
    {
        /// <inheritdoc/>
        private readonly IRespuestaHttpValidador _respuestaHttpValidador;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;

        public ServicioEjecutorHttp(IRespuestaHttpValidador respuestaHttpValidador, ISerializadorJsonServicio serializadorJsonServicio)
        {
            _respuestaHttpValidador = respuestaHttpValidador;
            _serializadorJsonServicio = serializadorJsonServicio;
        }

        public async Task<T> ObtenerRespuestaHttpAsync<TRequest, T>
            (Func<TRequest, Task<HttpResponseMessage>> funcionEjecutar, TRequest request)
        {
            var respuesta = await funcionEjecutar(request);
            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Utilidades.Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);
            var contenidoJson = await respuesta.Content.ReadAsStringAsync();
            var resultado = _serializadorJsonServicio.Deserializar<ApiResponseDto<T?>>(contenidoJson);

            return resultado.Data!;
        }

        public async Task<T> ObtenerRespuestaHttpAsync<T>(
            Func<Task<HttpResponseMessage>> funcionEjecutar)
        {
            var respuesta = await funcionEjecutar();
            await _respuestaHttpValidador.ValidarRespuesta(respuesta, Utilidades.Textos.Generales.MENSAJE_ERROR_CONSUMO_SERVICIO);
            var contenidoJson = await respuesta.Content.ReadAsStringAsync();
            var resultado = _serializadorJsonServicio.Deserializar<ApiResponseDto<T?>>(contenidoJson);

            return resultado.Data!;
        }
    }
}
