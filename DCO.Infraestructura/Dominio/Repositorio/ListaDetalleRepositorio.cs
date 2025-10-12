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

        public void MarcarCrear(DCO_ListaDetalle listaDetalle)
        {
            _context.DCO_ListasDetalles.Add(listaDetalle);
        }

        public void MarcarModificar(DCO_ListaDetalle listaDetalle)
        {
            _context.DCO_ListasDetalles.Update(listaDetalle);
        }

        public void MarcarEliminar(DCO_ListaDetalle listaDetalle)
        {
            _context.DCO_ListasDetalles.Remove(listaDetalle);
        }

        public async Task<DCO_ListaDetalle?> ObtenerPorIdAsync(int id)
        {
            return await _context.DCO_ListasDetalles.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<DCO_ListaDetalle?> ObtenerPorListaIdYCodigoAsync(int listaId, string codigo)
        {
            return await _context.DCO_ListasDetalles.FirstOrDefaultAsync(g => g.ListaId == listaId && g.Codigo == codigo);
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

        public IQueryable<ListaDetalleMV> ListarPorCodigoLista(string codigoLista)
        {
            return _context.DCO_ListasDetalles
                         .Include(ld => ld.Lista)
                         .Where(ld => ld.Lista.Codigo == codigoLista)
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
