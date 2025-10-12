using DCO.DataAccess;
using DCO.Dominio.Repositorio;
using DCO.Dominio.Entidades;

namespace DCO.Infraestructura.Dominio.Repositorio
{
    public class DatoConstanteDetalleRepositorio : IDatoConstanteDetalleRepositorio
    {
        private readonly AppDbContext _context;
        public DatoConstanteDetalleRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public void MarcarCrear(DCO_DatoConstanteDetalle datoConstanteDetalle)
        {
            _context.DCO_DatosConstantesDetalles.Add(datoConstanteDetalle);
        }

        public void MarcarModificar(DCO_DatoConstanteDetalle datoConstanteDetalle)
        {
            _context.DCO_DatosConstantesDetalles.Update(datoConstanteDetalle);
        }

        public void MarcarEliminar(DCO_DatoConstanteDetalle datoConstanteDetalle)
        {
            _context.DCO_DatosConstantesDetalles.Remove(datoConstanteDetalle);
        }
    }
}
