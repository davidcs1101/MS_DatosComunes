using Microsoft.EntityFrameworkCore;
using DCO.DataAccess;
using DCO.Dominio.Entidades;
using DCO.Dominio.Repositorio;

namespace DCO.Infraestructura.Dominio.Repositorio
{
    public class DatoConstanteRepositorio : IDatoConstanteRepositorio
    {
        private readonly AppDbContext _context;
        public DatoConstanteRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public void MarcarCrear(DCO_DatoConstante datoConstante)
        {
            _context.DCO_DatosConstantes.Add(datoConstante);
        }

        public void MarcarModificar(DCO_DatoConstante datoConstante)
        {
            _context.DCO_DatosConstantes.Update(datoConstante);
        }

        public void MarcarEliminar(DCO_DatoConstante datoConstante)
        {
            _context.DCO_DatosConstantes.Remove(datoConstante);
        }

        public async Task<DCO_DatoConstante?> ObtenerPorIdAsync(int id)
        {
            return await _context.DCO_DatosConstantes.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<DCO_DatoConstante?> ObtenerPorCodigoAsync(string codigo)
        {
            return await _context.DCO_DatosConstantes.FirstOrDefaultAsync(g => g.Codigo == codigo);
        }

        public IQueryable<DCO_DatoConstante> Listar()
        {
            return _context.DCO_DatosConstantes;
        }
    }
}
