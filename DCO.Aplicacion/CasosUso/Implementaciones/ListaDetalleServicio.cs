using DCO.Dtos;
using Microsoft.EntityFrameworkCore;
using DCO.Dominio.Repositorio;
using DCO.Aplicacion.CasosUso.Interfaces;
using AutoMapper;
using DCO.Dominio.Entidades.ModelosVistas;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Dominio.Servicios.Interfaces;
using Utilidades;
using static Utilidades.Textos;
using DCO.Dominio.Entidades;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class ListaDetalleServicio : IListaDetalleServicio
    {

        private readonly IMapper _mapper;
        private readonly IApiResponse _apiResponse;

        private readonly IListaRepositorio _listaRepositorio;
        private readonly IEntidadValidador<DCO_Lista> _listaValidador;

        private readonly IListaDetalleRepositorio _listaDetalleRepositorio;
        private readonly IEntidadValidador<ListaDetalleMV> _listaDetalleValidador;
        private readonly IDatoConstanteRepositorio _datoConstanteRepositorio;
        private readonly IEntidadValidador<DCO_DatoConstante> _datoConstanteValidador;

        public ListaDetalleServicio(IListaDetalleRepositorio listaDetalleRepositorio, IMapper mapper, IApiResponse apiResponseServicio, IEntidadValidador<ListaDetalleMV> entidadValidador, IEntidadValidador<ListaDetalleMV> listaDetalleValidador, IEntidadValidador<DCO_DatoConstante> datoConstanteValidador, IDatoConstanteRepositorio datoConstanteRepositorio, IListaRepositorio listaRepositorio, IEntidadValidador<DCO_Lista> listaValidador)
        {
            _listaDetalleRepositorio = listaDetalleRepositorio;
            _mapper = mapper;
            _apiResponse = apiResponseServicio;
            _listaDetalleValidador = listaDetalleValidador;
            _datoConstanteValidador = datoConstanteValidador;
            _datoConstanteRepositorio = datoConstanteRepositorio;
            _listaRepositorio = listaRepositorio;
            _listaValidador = listaValidador;
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

        public async Task<ApiResponse<ListaDetalleDto?>> ObtenerPorCodigoListaYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest)
        {
            var lista = await _listaRepositorio.ObtenerPorCodigoAsync(codigoDetalleRequest.Codigo);
            _listaValidador.ValidarDatoNoEncontrado(lista, Textos.Listas.MENSAJE_LISTA_NO_EXISTE_CODIGO);

            var listasDetallesMV = await _listaDetalleRepositorio.Listar()
                .Where(ld => ld.CodigoLista == codigoDetalleRequest.Codigo && ld.Codigo == codigoDetalleRequest.CodigoListaDetalle).FirstOrDefaultAsync();

            _listaDetalleValidador.ValidarDatoNoEncontrado(listasDetallesMV, Textos.ListasDetalles.MENSAJE_LISTADETALLE_NO_EXISTE_EN_CODIGOLISTA(codigoDetalleRequest.Codigo, codigoDetalleRequest.CodigoListaDetalle));
            var listasDetallesDto = _mapper.Map<ListaDetalleDto>(listasDetallesMV);

            return _apiResponse.CrearRespuesta<ListaDetalleDto?>(true, "", listasDetallesDto);
        }

        public async Task<ApiResponse<ListaDetalleDto?>> ObtenerPorCodigoConstanteYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest) 
        {
            var datoConstante = await _datoConstanteRepositorio.ObtenerPorCodigoAsync(codigoDetalleRequest.Codigo);
            _datoConstanteValidador.ValidarDatoNoEncontrado(datoConstante, Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_CODIGO);

            var listasDetallesMV = await _listaDetalleRepositorio.ListarPorCodigoConstante(codigoDetalleRequest.Codigo)
                .Where(ld => ld.Codigo == codigoDetalleRequest.CodigoListaDetalle).FirstOrDefaultAsync();

            _listaDetalleValidador.ValidarDatoNoEncontrado(listasDetallesMV, Textos.ListasDetalles.MENSAJE_LISTADETALLE_NO_EXISTE_EN_CODIGOCONSTANTE(codigoDetalleRequest.Codigo, codigoDetalleRequest.CodigoListaDetalle));
            var listasDetallesDto = _mapper.Map<ListaDetalleDto>(listasDetallesMV);

            return _apiResponse.CrearRespuesta<ListaDetalleDto?>(true, "", listasDetallesDto);
        }
    }
}
