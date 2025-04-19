using Microsoft.AspNetCore.Http;
using DCO.Aplicacion.ServiciosExternos;

namespace DCO.Infraestructura.Aplicacion.ServiciosExternos
{
    public class UsuarioContextoServicio : IUsuarioContextoServicio
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioContextoServicio(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Int32 ObtenerUsuarioIdToken()
        {
            // Obtener el 'UsuarioId' desde el token JWT en el contexto HTTP
            var usuarioIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("UsuarioId")?.Value;

            if (string.IsNullOrEmpty(usuarioIdClaim))
                throw new UnauthorizedAccessException("No se encontró el 'UsuarioId' en el token JWT.");

            return Convert.ToInt32(usuarioIdClaim);
        }
    }
}
