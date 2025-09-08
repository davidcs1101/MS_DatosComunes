using DCO.Dominio.Repositorio;
using DCO.DataAccess;
using DCO.Dominio.Entidades;

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

        public IQueryable<DCO_ColaSolicitud> Listar()
        {
            return _context.DCO_ColaSolicitudes;
        }
    }
}
