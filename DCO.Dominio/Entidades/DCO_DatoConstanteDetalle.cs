namespace DCO.Dominio.Entidades
{
    public class DCO_DatoConstanteDetalle : DCO_BaseAuditoria
    {
        public int Id { get; set; }
        public int DatoConstanteId { get; set; }
        public int ListaDetalleId { get; set; }

        public DCO_DatoConstante DatoConstante { get; set; } = null!;
        public DCO_ListaDetalle ListaDetalle { get; set; } = null!;
    }
}
