namespace DCO.Dominio.Repositorio.UnidadTrabajo
{
    public interface IUnidadDeTrabajo
    {
        Task<ITransaccion> IniciarTransaccionAsync();
        Task GuardarCambiosAsync();
    }
}