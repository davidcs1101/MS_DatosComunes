using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Dominio.Entidades.Configuraciones
{
    public class DCO_DatoConstanteDetalleConfig : IEntityTypeConfiguration<DCO_DatoConstanteDetalle>
    {
        public void Configure(EntityTypeBuilder<DCO_DatoConstanteDetalle> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FechaCreado).HasColumnType("datetime");
            builder.Property(x => x.FechaModificado).HasColumnType("datetime");

            builder.HasIndex(x => new { x.DatoConstanteId,x.DatoId }).IsUnique();

            builder.HasOne(x => x.DatoConstante)
                .WithMany(x => x.DatosConstantesDetalles)
                .HasForeignKey(x => x.DatoConstanteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
