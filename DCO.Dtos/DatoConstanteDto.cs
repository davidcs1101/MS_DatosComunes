﻿namespace DCO.Dtos
{
    public class DatoConstanteDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;

        public int UsuarioCreadorId { get; set; }
        public string? NombreUsuarioCreador { get; set; }
        public DateTime FechaCreado { get; set; }
        public int? UsuarioModificadorId { get; set; }
        public string? NombreUsuarioModificador { get; set; }
        public DateTime? FechaModificado { get; set; }
        public bool EstadoActivo { get; set; }
    }
}
