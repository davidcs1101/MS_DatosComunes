using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DCO.Dominio.Entidades;

namespace DCO.DataAccess.EntidadesConfig
{
    public class DCO_ColaSolicitudesConfig : IEntityTypeConfiguration<DCO_ColaSolicitud>
    {
        public void Configure(EntityTypeBuilder<DCO_ColaSolicitud> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Tipo).HasColumnType("varchar(250)").HasComment("Tipo de solicitud a realizar.");
            builder.Property(x => x.Payload).HasColumnType("Text").HasComment("Payload para la solicitud.");
            builder.Property(x => x.Estado).HasColumnType("smallint").HasComment("Estado del proceso de la solicitud. (0: Pendiente, 1: Procesando, 2: Exitosa, 3: Fallida).");
            builder.Property(x => x.Intentos).HasDefaultValue(0).HasComment("Intentos del proceso");
            builder.Property(x => x.FechaCreado).HasDefaultValueSql("CURRENT_TIMESTAMP").HasColumnType("datetime");
            builder.Property(x => x.FechaUltimoIntento).HasColumnType("datetime");
            builder.Property(x => x.ErrorMensaje).HasColumnType("Text").HasComment("Detalle de error de procasado de la solicitud.");

            builder.HasIndex(x => new { x.Tipo });
            builder.HasIndex(x => new { x.Estado });
        }
    }
}
