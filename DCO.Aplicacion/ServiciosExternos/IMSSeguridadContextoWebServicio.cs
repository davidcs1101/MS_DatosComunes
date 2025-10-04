using DCO.Dtos;
using Refit;

namespace DCO.Aplicacion.ServiciosExternos
{
    public interface IMSSeguridadContextoWebServicio
    {
        [Get("/usuarios/obtenerNombreUsuarioPorId")]
        Task<HttpResponseMessage> ObtenerNombreUsuarioPorIdAsync([Query] int id);
        [Post("/usuarios/listar")]
        Task<HttpResponseMessage> ObtenerNombresUsuariosPorIds([Body] IdsListadoDto usuarioIds);
    }
}
