namespace DCO.Dtos.AppSettings
{
    public class JWTSettings
    {
        public string? Emisor { get; set; }
        public string? Audiencia { get; set; }
        public string? Llave { get; set; }
        public string? MinutosDuracionTokenAutenticacionUsuario { get; set; }
        public string? MinutosDuracionTokenAutenticacionSede { get; set; }
        public List<string?> AudienciasDestino { get; set; } = new List<string?>();
    }

    public class TrabajosColasSettings
    {
        public string? ProcesarColaSolicitudesCron { get; set; }
        public string? CantidadIntentosPorRegistroEnCola { get; set; }
        public string? CantidadRegistrosProcesarIteracion { get; set; }
        public string? UsuarioIntegracion { get; set; }
        public string? ClaveIntegracion { get; set; }
    }

    public class EventosNotificarSettings
    {
        public List<string?> ActualizarListasDetalleServicios { get; set; } = new List<string?>();
    }

}
