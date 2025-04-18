using DCO.Dominio.Excepciones;
using DCO.Dominio.Servicios;

namespace DCO.Infraestructura.Dominio.Servicios
{
    public class EntidadValidador<TEntity> : IEntidadValidador<TEntity>
    {
        public void ValidarDatoYaExiste(TEntity? entidad, string mensaje)
        {
            if (entidad != null)
                throw new DatoYaExisteException(mensaje);
        }
        public void ValidarDatoNoEncontrado(TEntity? entidad, string mensaje)
        {
            if (entidad == null)
                throw new DatoNoEncontradoException(mensaje);
        }
    }
}
