using DCO.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DCO.DataAcces.EntidadesConfig
{
    public class DCO_DepartamentoConfig : IEntityTypeConfiguration<DCO_Departamento>
    {
        public void Configure(EntityTypeBuilder<DCO_Departamento> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Codigo).HasColumnType("varchar(5)");
            builder.Property(x => x.Nombre).HasColumnType("varchar(250)");
            builder.Property(x => x.Indicativo).HasColumnType("varchar(6)");
            builder.Property(x => x.FechaCreado).HasColumnType("datetime");
            builder.Property(x => x.FechaModificado).HasColumnType("datetime");

            builder.HasIndex(x => new { x.PaisId, x.Codigo }).IsUnique();

            builder.HasOne(x => x.Pais)
                .WithMany(x => x.Departamentos)
                .HasForeignKey(x => x.PaisId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
