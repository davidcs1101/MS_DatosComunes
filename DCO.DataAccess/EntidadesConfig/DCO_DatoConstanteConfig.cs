using DCO.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCO.DataAcces.EntidadesConfig
{
    public class DCO_DatoConstanteConfig : IEntityTypeConfiguration<DCO_DatoConstante>
    {
        public void Configure(EntityTypeBuilder<DCO_DatoConstante> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Codigo).HasColumnType("varchar(30)");
            builder.Property(x => x.Nombre).HasColumnType("varchar(250)");
            builder.Property(x => x.FechaCreado).HasColumnType("datetime");
            builder.Property(x => x.FechaModificado).HasColumnType("datetime");

            builder.HasIndex(x => new { x.Codigo }).IsUnique();

            builder.HasOne(x => x.Lista)
                .WithMany(x => x.DatosConstantes)
                .HasForeignKey(x => x.ListaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
