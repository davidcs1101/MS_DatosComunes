using DCO.Dominio.Excepciones;
using DCO.Dominio.Servicios.Interfaces;

namespace DCO.Dominio.Servicios.Implementaciones
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
