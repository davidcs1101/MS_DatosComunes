namespace DCO.Dominio.Entidades
{
    public class DCO_DatoConstante : DCO_BaseAuditoria
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool EstadoActivo { get; set; }

        public HashSet<DCO_DatoConstanteDetalle> DatosConstantesDetalles { get; set; } = new HashSet<DCO_DatoConstanteDetalle>();
    }
}
