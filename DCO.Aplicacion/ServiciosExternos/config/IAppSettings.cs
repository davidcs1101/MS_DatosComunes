using DCO.Dtos.AppSettings;
namespace DCO.Aplicacion.ServiciosExternos.config
{
    public interface IAppSettings
    {
        TrabajosColasSettings ObtenerTrabajosColasSettings();
        List<string?> ObtenerActualizarListasDetalleServicios();
        List<string?> ObtenerActualizarConstantesDetalleServicios();
    }
}
