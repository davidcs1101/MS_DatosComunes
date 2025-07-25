using DCO.Dtos;
using Microsoft.EntityFrameworkCore;
using DCO.Dominio.Repositorio;
using DCO.Aplicacion.CasosUso.Interfaces;
using AutoMapper;
using DCO.Dominio.Entidades.ModelosVistas;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Dominio.Servicios.Interfaces;
using Utilidades;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class ListaDetalleServicio : IListaDetalleServicio
    {
        private readonly IListaDetalleRepositorio _listaDetalleRepositorio;
        private readonly IMapper _mapper;
        private readonly IApiResponse _apiResponse;
        private readonly IEntidadValidador<ListaDetalleMV> _listaDetalleValidador;

        public ListaDetalleServicio(IListaDetalleRepositorio listaDetalleRepositorio, IMapper mapper, IApiResponse apiResponseServicio, IEntidadValidador<ListaDetalleMV> entidadValidador, IEntidadValidador<ListaDetalleMV> listaDetalleValidador)
        {
            _listaDetalleRepositorio = listaDetalleRepositorio;
            _mapper = mapper;
            _apiResponse = apiResponseServicio;
            _listaDetalleValidador = listaDetalleValidador;
        }

        public async Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.Listar()
                .Where(ld => ld.CodigoLista == codigoLista)
                .ToListAsync();
            var listasDetallesDto = _mapper.Map<List<ListaDetalleDto>>(listasDetallesMV);

            return _apiResponse.CrearRespuesta<List<ListaDetalleDto>?>(true, "", listasDetallesDto);
        }

        public async Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoConstanteAsync(string codigoConstante)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.ListarPorCodigoConstante(codigoConstante).ToListAsync();
            var listasDetallesDto = _mapper.Map<List<ListaDetalleDto>>(listasDetallesMV);

            return _apiResponse.CrearRespuesta<List<ListaDetalleDto>?>(true, "", listasDetallesDto);
        }

        public async Task<ApiResponse<string>> ValidarIdDetalleExisteEnCodigoListaAsync(CodigoListaIdDetalleRequest codigoListaIdDetalleRequest) 
        {
            var listaDetalle = await _listaDetalleRepositorio.Listar()
                .Where(ld => ld.CodigoLista == codigoListaIdDetalleRequest.CodigoLista && ld.Id == codigoListaIdDetalleRequest.Id)
                .FirstOrDefaultAsync();

            _listaDetalleValidador.ValidarDatoNoEncontrado(listaDetalle, Textos.ListasDetalles.MENSAJE_LISTADETALLE_NO_EXISTE_EN_CODIGOLISTA(codigoListaIdDetalleRequest.Id, codigoListaIdDetalleRequest.CodigoLista));

            return _apiResponse.CrearRespuesta(true, "", "");
        }
    }
}
