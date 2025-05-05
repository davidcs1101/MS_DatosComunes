namespace DCO.Dominio.Servicios.Interfaces
{
    public interface IEntidadValidador<TEntity>
    {
        void ValidarDatoYaExiste(TEntity? entidad, string mensaje);
        void ValidarDatoNoEncontrado(TEntity? entidad, string mensaje);
    }
}
