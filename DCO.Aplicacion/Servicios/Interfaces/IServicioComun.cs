namespace DCO.Aplicacion.Servicios.Interfaces
{
    public interface IServicioComun
    {
        Task<TReturn> ObtenerIdListaDetalleAsync<TRequest, TSerializacion, TReturn>
            (Func<TRequest, Task<HttpResponseMessage>> funcionEjecutar
            , TRequest request
            , Func<TSerializacion, TReturn> obtenerValor
            );
    }
}
