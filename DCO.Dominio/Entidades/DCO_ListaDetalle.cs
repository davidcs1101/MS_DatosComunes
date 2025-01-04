namespace DCO.Dominio.Entidades
{
    public class DCO_ListaDetalle : DCO_BaseAuditoria
    {
        public int Id { get; set; }
        public int ListaId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool EstadoActivo { get; set; }

        public DCO_Lista Lista { get; set; } = null!;
    }
}
