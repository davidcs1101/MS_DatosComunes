using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCO.DataAccess;
using DCO.Dominio.Entidades;
using DCO.Repositorio.Interfaces;
using DCO.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DCO.Repositorio.Implementaciones
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

        public async Task ModificarAsync(DCO_ListaDetalle listaDetalle)
        {
            _context.DCO_ListasDetalles.Update(listaDetalle);
            await _context.SaveChangesAsync();
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

        public IQueryable<ListaDetalleDto> Listar()
        {
            var listasDetalle = _context.DCO_ListasDetalles
                         .Include(ld => ld.Lista)
                         .Select(ld => new ListaDetalleDto
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
            return listasDetalle;
        }

        public IQueryable<ListaDetalleDto> ListarPorCodigoConstante(string codigoDatoConstante)
        {
            var datosConstantesDetalles = from dc in _context.DCO_DatosConstantes
                                          join dcd in _context.DCO_DatosConstantesDetalles on dc.Id equals dcd.DatoConstanteId
                                          join ld in _context.DCO_ListasDetalles on dcd.DatoId equals ld.Id
                                          where dc.Codigo == codigoDatoConstante
                                          select new ListaDetalleDto
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
            return datosConstantesDetalles;
        }
    }
}
