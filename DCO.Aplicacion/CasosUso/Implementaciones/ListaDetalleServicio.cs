using DCO.Dtos;
using Microsoft.EntityFrameworkCore;
using DCO.Dominio.Repositorio;
using DCO.Aplicacion.CasosUso.Interfaces;
using AutoMapper;
using DCO.Dominio.Entidades.ModelosVistas;
using DCO.Aplicacion.Servicios.Interfaces;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class ListaDetalleServicio : IListaDetalleServicio
    {
        private readonly IListaDetalleRepositorio _listaDetalleRepositorio;
        private readonly IMapper _mapper;
        private readonly IApiResponse _apiResponseServicio;

        public ListaDetalleServicio(IListaDetalleRepositorio listaDetalleRepositorio, IMapper mapper, IApiResponse apiResponseServicio)
        {
            _listaDetalleRepositorio = listaDetalleRepositorio;
            _mapper = mapper;
            _apiResponseServicio = apiResponseServicio;
        }

        public async Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.Listar()
                .Where(ld => ld.CodigoLista == codigoLista)
                .ToListAsync();
            var listasDetallesDto = _mapper.Map<List<ListaDetalleDto>>(listasDetallesMV);

            return _apiResponseServicio.CrearRespuesta<List<ListaDetalleDto>?>(true, "", listasDetallesDto);
        }

        public async Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoConstanteAsync(string codigoConstante)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.ListarPorCodigoConstante(codigoConstante).ToListAsync();
            var listasDetallesDto = _mapper.Map<List<ListaDetalleDto>>(listasDetallesMV);

            return _apiResponseServicio.CrearRespuesta<List<ListaDetalleDto>?>(true, "", listasDetallesDto);
        }
    }
}
