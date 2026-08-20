using Utilidades.Dtos;
using Utilidades.Dtos.Seguridad;
namespace DCO.Aplicacion.Servicios.Interfaces.Cache
{
    public interface ISeguridadPermisosCache
    {
        Task InicializarAsync();
        ApiResponseDto<string> Actualizar(List<AutorizacionDto> autorizaciones);
        bool TienePermiso(string codigoGrupo, string codigoPermiso);
        Task RefrescarAsync();
    }
}
