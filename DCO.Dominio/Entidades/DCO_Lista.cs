namespace DCO.Dominio.Entidades
{
    public class DCO_Lista : DCO_BaseAuditoria
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public List<DCO_ListaDetalle> ListasDetalles { get; set; } = new List<DCO_ListaDetalle>();
        public List<DCO_DatoConstante> DatosConstantes { get; set; } = new List<DCO_DatoConstante>();
    }
}
