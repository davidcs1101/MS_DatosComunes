using Microsoft.EntityFrameworkCore;
using DCO.Dominio.Entidades;
using DCO.Dominio.Entidades.Configuraciones;
using DCO.Dominio.Entidades.Semilla;
using System.Reflection;

namespace DCO.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.ApplyConfiguration(new DCO_ListaConfig());
            modelBuilder.ApplyConfiguration(new DCO_ListaDetalleConfig());
            modelBuilder.ApplyConfiguration(new DCO_DatoConstanteConfig());
            modelBuilder.ApplyConfiguration(new DCO_DatoConstanteDetalleConfig());

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
    }
}
