namespace DCO.Dominio.Entidades
{
    public class DCO_DatoConstanteDetalle : DCO_BaseAuditoria
    {
        public int Id { get; set; }
        public int DatoConstanteId { get; set; }
        public int DatoId { get; set; }
        public bool EstadoActivo { get; set; }

        public DCO_DatoConstante DatoConstante { get; set; } = null!;
    }
}
