using DCO.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.DataAcces.EntidadesConfig
{
    public class DCO_ListaDetalleConfig : IEntityTypeConfiguration<DCO_ListaDetalle>
    {
        public void Configure(EntityTypeBuilder<DCO_ListaDetalle> builder) 
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Codigo).HasColumnType("varchar(30)");
            builder.Property(x => x.Nombre).HasColumnType("varchar(250)");
            builder.Property(x => x.FechaCreado).HasColumnType("datetime");
            builder.Property(x => x.FechaModificado).HasColumnType("datetime");

            builder.HasIndex(x => new { x.ListaId, x.Codigo }).IsUnique();

            builder.HasOne(x => x.Lista)
                .WithMany(x => x.ListasDetalles)
                .HasForeignKey(x => x.ListaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
