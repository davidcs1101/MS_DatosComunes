namespace DCO.Dominio.Entidades
{
    public class DCO_Pais : DCO_BaseAuditoria
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public short Indicativo { get; set; }

        public List<DCO_Departamento> Departamentos { get; set; } = new List<DCO_Departamento>();
    }
}
