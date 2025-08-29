using DCO.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCO.DataAcces.EntidadesConfig
{
    public class DCO_BarrioConfig : IEntityTypeConfiguration<DCO_Barrio>
    {
        public void Configure(EntityTypeBuilder<DCO_Barrio> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Codigo).HasColumnType("varchar(5)");
            builder.Property(x => x.Nombre).HasColumnType("varchar(250)");
            builder.Property(x => x.FechaCreado).HasColumnType("datetime");
            builder.Property(x => x.FechaModificado).HasColumnType("datetime");

            builder.HasIndex(x => new { x.MunicipioId, x.Codigo }).IsUnique();

            builder.HasOne(x => x.Municipio)
                .WithMany(x => x.Barrios)
                .HasForeignKey(x => x.MunicipioId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
