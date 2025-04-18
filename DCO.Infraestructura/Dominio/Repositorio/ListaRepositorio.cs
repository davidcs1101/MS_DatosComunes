using DCO.DataAccess;
using DCO.Dominio.Entidades;
using DCO.Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace DCO.Infraestructura.Dominio.Repositorio
{
    public class ListaRepositorio : IListaRepositorio
    {
        private readonly AppDbContext _context;
        public ListaRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CrearAsync(DCO_Lista lista)
        {
            _context.DCO_Listas.Add(lista);
            await _context.SaveChangesAsync();
            return lista.Id;
        }

        public async Task ModificarAsync(DCO_Lista lista)
        {
            _context.DCO_Listas.Update(lista);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var eliminado = await _context.DCO_Listas.Where(u => u.Id == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<DCO_Lista?> ObtenerPorIdAsync(int id)
        {
            return await _context.DCO_Listas.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<DCO_Lista?> ObtenerPorCodigoAsync(string codigo)
        {
            return await _context.DCO_Listas.FirstOrDefaultAsync(g => g.Codigo == codigo);
        }

        public IQueryable<DCO_Lista> Listar()
        {
            return _context.DCO_Listas;
        }
    }
}
