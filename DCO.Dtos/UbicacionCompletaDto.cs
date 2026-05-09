namespace DCO.Dtos
{
    public class UbicacionCompletaDto
    {
        public int PaisId { get; set; }
        public string CodigoPais { get; set; } = null!;
        public string NombrePais { get; set; } = null!;
        public string IndicativoPais { get; set; } = null!;
        public bool EstadoPais { get; set; }

        public int DepartamentoId { get; set; }
        public string CodigoDepartamento { get; set; } = null!;
        public string NombreDepartamento { get; set; } = null!;
        public string IndicativoDepartamento { get; set; } = null!;
        public bool EstadoDepartamento { get; set; }

        public int MunicipioId { get; set; }
        public string CodigoMunicipio { get; set; } = null!;
        public string NombreMunicipio { get; set; } = null!;
        public bool EstadoMunicipio { get; set; }
    }
}
