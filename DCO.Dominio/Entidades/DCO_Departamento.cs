namespace DCO.Dominio.Entidades
{
    public class DCO_Departamento : DCO_BaseAuditoria
    {
        public int Id { get; set; }
        public int PaisId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Indicativo { get; set; } = null!;

        public DCO_Pais Pais { get; set; } = null!;
        public List<DCO_Municipio> Municipios { get; set; } = new List<DCO_Municipio>();
    }
}
