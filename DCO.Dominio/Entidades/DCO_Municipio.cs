namespace DCO.Dominio.Entidades
{
    public class DCO_Municipio : DCO_BaseAuditoria
    {
        public int Id { get; set; }
        public int DepartamentoId { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public DCO_Departamento Departamento { get; set; } = null!;
        public List<DCO_Barrio> Barrios { get; set; } = new List<DCO_Barrio>();
    }
}
