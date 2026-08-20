namespace DCO.Aplicacion.Servicios.Interfaces
{
    public interface IServicioEjecutorHttp
    {
        /// <summary>
        /// Este metodo se encarga de procesar las solicitides a servicios externos, toma 2 parametros, el primero
        /// se refiere al objeto o dato con el cual se lanza la solicitud. El segundo parametro se refiere al objeto 
        /// o dato que va a retornar dicha solicitud
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="funcionEjecutar"></param>
        /// <param name="request"></param>
        /// <returns>Retorna un tipo que se define dinamicamente en la llamada</returns>
        Task<T> ObtenerRespuestaHttpAsync<TRequest, T>
            (Func<TRequest, Task<HttpResponseMessage>> funcionEjecutar, TRequest request);

        Task<T> ObtenerRespuestaHttpAsync<T>(
            Func<Task<HttpResponseMessage>> funcionEjecutar);
    }
}
