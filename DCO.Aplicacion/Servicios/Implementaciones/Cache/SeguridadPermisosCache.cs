using Microsoft.Extensions.DependencyInjection;
using DCO.Aplicacion.Servicios.Interfaces.Cache;
using Utilidades.Dtos;
using Utilidades.Dtos.Seguridad;
using Utilidades.Servicios.Responses.Interfaces;

namespace DCO.Aplicacion.Servicios.Implementaciones.Cache
{
    public class SeguridadPermisosCache : ISeguridadPermisosCache
    {
        private readonly object _lock = new();
        private readonly IServiceScopeFactory _scopeFactory;
        private Dictionary<string, HashSet<string>> _permisos = new Dictionary<string, HashSet<string>>();
        private readonly IApiResponse _apiResponse;

        public SeguridadPermisosCache(IApiResponse apiResponse, IServiceScopeFactory scopeFactory)
        {
            _apiResponse = apiResponse;
            _scopeFactory = scopeFactory;
        }

        public async Task InicializarAsync()
        {
            await InicializarPermisosAsync();
        }

        public ApiResponseDto<string> Actualizar(List<AutorizacionDto> autorizaciones)
        {
            var permisos = autorizaciones
                .Where(x => x.EstadoPrograma && x.EstadoGrupo && x.EstadoPermiso && x.EstadoGrupoPermiso)
                .GroupBy(x => x.CodigoGrupo)
                .ToDictionary(
                    g => g.Key,
                    g => g
                        .Select(x => x.CodigoPermiso)
                        .ToHashSet(StringComparer.OrdinalIgnoreCase));

            lock (_lock)
                _permisos = permisos;

            return _apiResponse.CrearRespuesta(true,"Cache de permisos actualizada.","");
        }

        public bool TienePermiso(string codigoGrupo,string codigoPermiso)
        {
            lock (_lock)
            {
                if (_permisos.TryGetValue(codigoGrupo, out var permisos))
                    return permisos.Contains(codigoPermiso);

                return false;
            }
        }

        public async Task RefrescarAsync()
        {
            await ObtenerListaCatalogosAutorizacionAsync();
        }



        private async Task InicializarPermisosAsync()
        {
            lock (_lock)
            {
                if (_permisos.Count > 0)
                    return;
            }

            await ObtenerListaCatalogosAutorizacionAsync();
        }
        private async Task ObtenerListaCatalogosAutorizacionAsync() 
        {
            //using var scope = _scopeFactory.CreateScope();
            //var autorizacionServicio = scope.ServiceProvider.GetRequiredService<IAutorizacionServicio>();

            //var autorizaciones = await autorizacionServicio.ListarCatalogoAutorizacionAsync();

            //Actualizar(autorizaciones);
        }

    }
} 