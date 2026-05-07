using Microsoft.EntityFrameworkCore;
using DCO.Dominio.Entidades;
using DCO.DataAcces.EntidadesConfig;
using DCO.DataAcces.Semilla;

namespace DCO.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            DCO_DatosIniciales.Seed(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveColumnType("varchar");
        }

        public DbSet<DCO_Lista> DCO_Listas { get; set; }
        public DbSet<DCO_ListaDetalle> DCO_ListasDetalles { get; set; }
        public DbSet<DCO_DatoConstante> DCO_DatosConstantes { get; set; }
        public DbSet<DCO_DatoConstanteDetalle> DCO_DatosConstantesDetalles { get; set; }
        public DbSet<DCO_ColaSolicitud> DCO_ColaSolicitudes { get; set; }
        public DbSet<DCO_Pais> DCO_Paises { get; set; }
        public DbSet<DCO_Departamento> DCO_Departamentos { get; set; }
        public DbSet<DCO_Municipio> DCO_Municipios { get; set; }
    }
}
