namespace DCO.Dominio.Entidades
{
    public class DCO_DatoConstante : DCO_BaseAuditoria
    {
        public int Id { get; set; }
        public int ListaId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public DCO_Lista Lista { get; set; } = null!;
        public List<DCO_DatoConstanteDetalle> DatosConstantesDetalles { get; set; } = new List<DCO_DatoConstanteDetalle>();
    }
}
