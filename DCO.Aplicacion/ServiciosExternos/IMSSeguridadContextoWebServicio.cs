using DCO.Dtos;
using Refit;

namespace DCO.Aplicacion.ServiciosExternos
{
    public interface IMSSeguridadContextoWebServicio
    {
        [Get("/seg/usuarios/obtenerNombreUsuarioPorId")]
        Task<HttpResponseMessage> ObtenerNombreUsuarioPorIdAsync([Query] int id);
        [Post("/seg/usuarios/listar")]
        Task<HttpResponseMessage> ObtenerNombresUsuariosPorIds([Body] IdsListadoDto usuarioIds);
    }
}
