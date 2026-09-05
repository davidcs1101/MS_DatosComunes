using Microsoft.AspNetCore.Authorization;
using DCO.Aplicacion.Servicios.Interfaces.Cache;
using DCO.Aplicacion.ServiciosExternos;
using Utilidades;
using Utilidades.Excepciones;
using Utilidades.Servicios.Http.Interfaces;
namespace DCO.Api.DatosComunes.Middlewares.Permisos
{
    public class PermisoManejadorAutorizacion : AuthorizationHandler<PermisoRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISeguridadPermisosCache _permisosCache;

        public PermisoManejadorAutorizacion(IHttpContextAccessor httpContextAccessor, ISeguridadPermisosCache permisosCache)
        {
            _httpContextAccessor = httpContextAccessor;
            _permisosCache = permisosCache;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,PermisoRequirement requirement)
        {
            var endpoint = _httpContextAccessor.HttpContext?.GetEndpoint();

            if (endpoint == null)
                return;

            var permiso = endpoint.Metadata.GetMetadata<PermisoAttribute>();

            if (permiso == null)
                return;

            var codigoPermiso = permiso.Permiso;

            var usuarioContextoServicio = _httpContextAccessor.HttpContext!
                .RequestServices
                .GetRequiredService<IUsuarioContextoServicio>();

            var codigoGrupo = usuarioContextoServicio.ObtenerCodigoGrupo();

            var autorizado = _permisosCache.TienePermiso(codigoGrupo, codigoPermiso);

            if (autorizado)
                context.Succeed(requirement);
            else
                throw new PermisoNoAutorizadoException(Textos.Generales.MENSAJE_PERMISO_NO_AUTORIZADO(codigoPermiso, codigoGrupo));
        }
    }
}
