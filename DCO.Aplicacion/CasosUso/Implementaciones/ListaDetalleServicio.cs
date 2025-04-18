using DCO.Dtos;
using Microsoft.EntityFrameworkCore;
using DCO.Dominio.Repositorio;
using DCO.Aplicacion.CasosUso.Interfaces;
using AutoMapper;
using DCO.Dominio.Entidades.ModelosVistas;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class ListaDetalleServicio : IListaDetalleServicio
    {
        private readonly IListaDetalleRepositorio _listaDetalleRepositorio;
        private readonly IMapper _mapper;
        public ListaDetalleServicio(IListaDetalleRepositorio listaDetalleRepositorio, IMapper mapper)
        {
            _listaDetalleRepositorio = listaDetalleRepositorio;
            _mapper = mapper;
        }

        public async Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.Listar()
                .Where(ld => ld.CodigoLista == codigoLista)
                .ToListAsync();
            var listasDetalles = _mapper.Map<List<ListaDetalleDto>>(listasDetallesMV);

            return new ApiResponse<List<ListaDetalleDto>?> { Correcto = true, Mensaje = "", Data = listasDetalles };
        }

        public async Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoConstanteAsync(string codigoConstante)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.ListarPorCodigoConstante(codigoConstante).ToListAsync();
            var listasDetalles = _mapper.Map<List<ListaDetalleDto>>(listasDetallesMV);

            return new ApiResponse<List<ListaDetalleDto>?> { Correcto = true, Mensaje = "", Data = listasDetalles };
        }
    }
}
