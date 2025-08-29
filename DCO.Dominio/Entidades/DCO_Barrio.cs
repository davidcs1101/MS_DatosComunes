namespace DCO.Dominio.Entidades
{
    public class DCO_Barrio : DCO_BaseAuditoria
    {
        public int Id { get; set; }
        public int MunicipioId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public DCO_Municipio Municipio { get; set; } = null!;
    }
}
