using DCO.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCO.DataAcces.EntidadesConfig
{
    public class DCO_DatoConstanteDetalleConfig : IEntityTypeConfiguration<DCO_DatoConstanteDetalle>
    {
        public void Configure(EntityTypeBuilder<DCO_DatoConstanteDetalle> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FechaCreado).HasColumnType("datetime");
            builder.Property(x => x.FechaModificado).HasColumnType("datetime");

            builder.HasIndex(x => new { x.DatoConstanteId,x.ListaDetalleId }).IsUnique();

            builder.HasOne(x => x.DatoConstante)
                .WithMany(x => x.DatosConstantesDetalles)
                .HasForeignKey(x => x.DatoConstanteId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ListaDetalle)
                .WithMany(x => x.DatosConstantesDetalles)
                .HasForeignKey(x => x.ListaDetalleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
