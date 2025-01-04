using DCO.Dtos;
using DCO.Servicio.Interfaces;
using DCO.Repositorio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DCO.Servicio.Implementaciones
{
    public class ListaDetalleServicio : IListaDetalleServicio
    {
        private readonly IListaDetalleRepositorio _listaDetalleRepositorio;
        public ListaDetalleServicio(IListaDetalleRepositorio listaDetalleRepositorio) 
        {
            _listaDetalleRepositorio = listaDetalleRepositorio;
        }

        public async Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista)
        {
            var listasDetalles = await _listaDetalleRepositorio.Listar()
                .Where(ld => ld.CodigoLista == codigoLista)
                .ToListAsync();

            return new ApiResponse<List<ListaDetalleDto>?> { Correcto = true, Mensaje = "", Data = listasDetalles };
        }

        public async Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoConstanteAsync(string codigoConstante)
        {
            var listasDetalles = await _listaDetalleRepositorio.ListarPorCodigoConstante(codigoConstante).ToListAsync();

            return new ApiResponse<List<ListaDetalleDto>?> { Correcto = true, Mensaje = "", Data = listasDetalles };
        }
    }
}
