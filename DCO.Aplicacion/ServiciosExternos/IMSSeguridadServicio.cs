using DCO.Dtos;

namespace DCO.Aplicacion.ServiciosExternos
{
    public interface IMSSeguridadServicio
    {
        Task<HttpResponseMessage> ObtenerNombreUsuarioPorIdAsync(int id);
        Task<HttpResponseMessage> ObtenerNombresUsuariosPorIds(IdsListadoDto usuarioIds);
    }
}
