namespace DCO.Dominio.Entidades
{
    public class DCO_BaseAuditoria
    {
        public int UsuarioCreadorId { get; set; }
        public DateTime FechaCreado { get; set; } = DateTime.Now;
        public int? UsuarioModificadorId { get; set; }
        public DateTime? FechaModificado { get; set; }
        public bool EstadoActivo { get; set; } = true;
    }
}
