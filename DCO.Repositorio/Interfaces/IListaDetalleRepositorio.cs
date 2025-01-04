using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCO.Dominio;
using DCO.Dominio.Entidades;
using DCO.Dtos;

namespace DCO.Repositorio.Interfaces
{
    public interface IListaDetalleRepositorio
    {
        Task<int> CrearAsync(DCO_ListaDetalle listaDetalle);
        Task ModificarAsync(DCO_ListaDetalle listaDetalle);
        Task<bool> EliminarAsync(int id);
        Task<DCO_ListaDetalle?> ObtenerPorIdAsync(int id);
        Task<DCO_ListaDetalle?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<ListaDetalleDto> Listar();
        IQueryable<ListaDetalleDto> ListarPorCodigoConstante(string codigoDatoConstante);
    }
}
