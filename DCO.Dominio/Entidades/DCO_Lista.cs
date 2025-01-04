namespace DCO.Dominio.Entidades
{
    public class DCO_Lista : DCO_BaseAuditoria
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public bool EstadoActivo { get; set; }

        public HashSet<DCO_ListaDetalle> ListasDetalles { get; set; } = new HashSet<DCO_ListaDetalle>();
    }
}
