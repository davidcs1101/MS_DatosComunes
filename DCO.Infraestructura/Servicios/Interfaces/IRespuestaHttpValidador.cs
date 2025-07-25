namespace DCO.Infraestructura.Servicios.Interfaces
{
    public interface IRespuestaHttpValidador
    {
        Task ValidarRespuesta(HttpResponseMessage respuesta, string mensaje);
    }
}
