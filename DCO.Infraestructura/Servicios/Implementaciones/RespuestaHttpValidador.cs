using DCO.Dominio.Excepciones;
using DCO.Infraestructura.Servicios.Interfaces;

namespace DCO.Infraestructura.Servicios.Implementaciones
{
    public class RespuestaHttpValidador : IRespuestaHttpValidador
    {
        public void ValidarRespuesta(HttpResponseMessage respuesta, string mensaje) {
            if (!respuesta.IsSuccessStatusCode)
                throw new SolicitudHttpException($"{mensaje} : {respuesta.ReasonPhrase}");
        }
    }
}
