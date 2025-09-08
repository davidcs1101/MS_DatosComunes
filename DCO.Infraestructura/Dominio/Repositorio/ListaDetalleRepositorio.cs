using Microsoft.EntityFrameworkCore;
using DCO.DataAccess;
using DCO.Dominio.Repositorio;
using DCO.Dominio.Entidades;
using DCO.Dominio.Entidades.ModelosVistas;

namespace DCO.Infraestructura.Dominio.Repositorio
{
    public class ListaDetalleRepositorio : IListaDetalleRepositorio
    {
        private readonly AppDbContext _context;
        public ListaDetalleRepositorio(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<int> CrearAsync(DCO_ListaDetalle listaDetalle)
        {
            _context.DCO_ListasDetalles.Add(listaDetalle);
            await _context.SaveChangesAsync();
            return listaDetalle.Id;
        }

        public void MarcarCrear(DCO_ListaDetalle listaDetalle)
        {
            _context.DCO_ListasDetalles.Add(listaDetalle);
        }

        public async Task ModificarAsync(DCO_ListaDetalle listaDetalle)
        {
            _context.DCO_ListasDetalles.Update(listaDetalle);
            await _context.SaveChangesAsync();
        }

        public void MarcarModificar(DCO_ListaDetalle listaDetalle)
        {
            _context.DCO_ListasDetalles.Update(listaDetalle);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var eliminado = await _context.DCO_ListasDetalles.Where(u => u.Id == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<DCO_ListaDetalle?> ObtenerPorIdAsync(int id)
        {
            return await _context.DCO_ListasDetalles.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<DCO_ListaDetalle?> ObtenerPorCodigoAsync(string codigo)
        {
            return await _context.DCO_ListasDetalles.FirstOrDefaultAsync(g => g.Codigo == codigo);
        }

        public IQueryable<ListaDetalleMV> Listar()
        {
            return _context.DCO_ListasDetalles
                         .Include(ld => ld.Lista)
                         .Select(ld => new ListaDetalleMV
                         {
                             Id = ld.Id,
                             ListaId = ld.ListaId,
                             Codigo = ld.Codigo,
                             Nombre = ld.Nombre,
                             UsuarioCreadorId = ld.UsuarioCreadorId,
                             FechaCreado = ld.FechaCreado,
                             UsuarioModificadorId = ld.UsuarioModificadorId,
                             FechaModificado = ld.FechaModificado,
                             EstadoActivo = ld.EstadoActivo,

                             CodigoLista = ld.Lista.Codigo
                         });
        }

        public IQueryable<ListaDetalleMV> ListarPorCodigoConstante(string codigoDatoConstante)
        {
            return from dc in _context.DCO_DatosConstantes
                                          join dcd in _context.DCO_DatosConstantesDetalles on dc.Id equals dcd.DatoConstanteId
                                          join ld in _context.DCO_ListasDetalles on dcd.ListaDetalleId equals ld.Id
                                          where dc.Codigo == codigoDatoConstante
                                          select new ListaDetalleMV
                                          {
                                              Id = ld.Id,
                                              ListaId = ld.ListaId,
                                              Codigo = ld.Codigo,
                                              Nombre = ld.Nombre,
                                              UsuarioCreadorId = ld.UsuarioCreadorId,
                                              FechaCreado = ld.FechaCreado,
                                              UsuarioModificadorId = ld.UsuarioModificadorId,
                                              FechaModificado = ld.FechaModificado,
                                              EstadoActivo = ld.EstadoActivo,

                                              CodigoDatoConstante = dc.Codigo
                                          };
        }
    }
}
