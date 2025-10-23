using DCO.DataAccess;
using DCO.Dominio.Repositorio;
using DCO.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

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

        public async Task<DCO_DatoConstanteDetalle?> ObtenerPorId(int id)
        {
            return await _context.DCO_DatosConstantesDetalles
                .Include(d => d.DatoConstante)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<DCO_DatoConstanteDetalle?> ObtenerPorDatoConstanteIdYListaDetalleIdAsync(int datoConstanteId, int listaDetalleId)
        {
            return await _context.DCO_DatosConstantesDetalles.FirstOrDefaultAsync(g => g.DatoConstanteId == datoConstanteId && g.ListaDetalleId == listaDetalleId);
        }
    }
}
