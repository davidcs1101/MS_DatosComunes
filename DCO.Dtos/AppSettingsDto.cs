namespace DCO.Dtos.AppSettings
{
    public class JWTSettings
    {
        public string? Emisor { get; set; }
        public string? Audiencia { get; set; }
        public string? Llave { get; set; }
        public List<string?> AudienciasDestino { get; set; } = new List<string?>();
    }

    public class TrabajosColasSettings
    {
        public int CantidadIntentosPorRegistroEnCola { get; set; } = 0; //Cantidad de Reintentos para procesar un registro en la cola antes de marcarlo como error.
        public string ProcesarColaSolicitudesCron { get; set; } = "*/5 * * * *";
        public int CantidadRegistrosProcesarIteracion { get; set; } = 10;
        public string UsuarioIntegracion { get; set; } = "";
        public string ClaveIntegracion { get; set; } = "";
    }

    public class EventosNotificarSettings
    {
        public List<string?> ActualizarListasDetalleServicios { get; set; } = new List<string?>();
        public List<string?> ActualizarConstantesDetalleServicios { get; set; } = new List<string?>();
    }
}
