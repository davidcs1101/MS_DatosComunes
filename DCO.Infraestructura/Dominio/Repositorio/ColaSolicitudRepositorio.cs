using DCO.DataAccess;
using DCO.Dominio.Entidades;
using DCO.Dominio.Enumeraciones;
using DCO.Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace DCO.Intraestructura.Dominio.Repositorio
{
    public class ColaSolicitudRepositorio : IColaSolicitudRepositorio
    {
        private readonly AppDbContext _context;

        public ColaSolicitudRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public void MarcarCrear(DCO_ColaSolicitud colaSolicitud)
        {
            _context.DCO_ColaSolicitudes.Add(colaSolicitud);
        }

        public void MarcarModificar(DCO_ColaSolicitud colaSolicitud)
        {

            _context.DCO_ColaSolicitudes.Update(colaSolicitud);
        }

        public async Task<DCO_ColaSolicitud?> ObtenerPorIdAsync(int id) 
        {
            return await _context.DCO_ColaSolicitudes.FindAsync(id);
        }

        public async Task<List<DCO_ColaSolicitud>> ListarAsync(EstadoCola estado, int cantidadRegistros)
        {
            return await _context.DCO_ColaSolicitudes
                .Where(c => c.Estado == estado)
                .OrderBy(c => c.Id)
                .Take(cantidadRegistros)
                .ToListAsync();
        }

        public async Task<int> CrearAsync(DCO_ColaSolicitud colaSolicitud)
        {
            _context.DCO_ColaSolicitudes.Add(colaSolicitud);
            await _context.SaveChangesAsync();
            return colaSolicitud.Id;
        }
    }
}
