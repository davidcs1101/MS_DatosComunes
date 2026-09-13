using DCO.Dtos;
using DCO.Dominio.Repositorio;
using DCO.Aplicacion.CasosUso.Interfaces;
using AutoMapper;
using DCO.Dominio.Entidades.ModelosVistas;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Dominio.Servicios.Interfaces;
using Utilidades;
using DCO.Dominio.Entidades;
using DCO.Dominio.Repositorio.UnidadTrabajo;
using DCO.Aplicacion.ServiciosExternos.config;
using Utilidades.Dtos;
using Utilidades.Servicios.Responses.Interfaces;
using Utilidades.Servicios.Http.Interfaces;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class ListaDetalleServicio : IListaDetalleServicio
    {

        private readonly IMapper _mapper;
        private readonly IApiResponse _apiResponse;
        private readonly IListaRepositorio _listaRepositorio;
        private readonly IEntidadValidador<DCO_Lista> _listaValidador;
        private readonly IListaDetalleRepositorio _listaDetalleRepositorio;
        private readonly IEntidadValidador<DCO_ListaDetalle> _listaDetalleValidador;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IAppSettings _appSettings;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IProcesadorTransacciones _procesadorTransacciones;
        private readonly IColaSolicitudServicio _colaSolicitudServicio;
        private readonly ISincronizadorMicroservicios _sincronizadorMicroservicios;

        public ListaDetalleServicio(IListaDetalleRepositorio listaDetalleRepositorio, IMapper mapper, IApiResponse apiResponseServicio, IEntidadValidador<ListaDetalleMV> entidadValidador, IListaRepositorio listaRepositorio, IEntidadValidador<DCO_Lista> listaValidador, IUnidadDeTrabajo unidadDeTrabajo,
            IUsuarioContextoServicio usuarioContextoServicio, IEntidadValidador<DCO_ListaDetalle> listaDetalleValidador, IProcesadorTransacciones procesadorTransacciones, IAppSettings appSettings, IColaSolicitudServicio colaSolicitudServicio, ISincronizadorMicroservicios sincronizadorMicroservicios)
        {
            _listaDetalleRepositorio = listaDetalleRepositorio;
            _mapper = mapper;
            _apiResponse = apiResponseServicio;
            _listaRepositorio = listaRepositorio;
            _listaValidador = listaValidador;
            _unidadDeTrabajo = unidadDeTrabajo;
            _usuarioContextoServicio = usuarioContextoServicio;
            _listaDetalleValidador = listaDetalleValidador;
            _procesadorTransacciones = procesadorTransacciones;
            _appSettings = appSettings;
            _colaSolicitudServicio = colaSolicitudServicio;
            _sincronizadorMicroservicios = sincronizadorMicroservicios;
        }

        public async Task<ApiResponseDto<int>> CrearAsync(ListaDetalleCreacionRequest listaDetalleCreacionRequest)
        {
            var id = 0;
            var colas = new List<DCO_ColaSolicitud>();
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var listaExiste = await _listaRepositorio.ObtenerPorCodigoAsync(listaDetalleCreacionRequest.CodigoLista);
                _listaValidador.ValidarDatoNoEncontrado(listaExiste, Textos.Listas.MENSAJE_LISTA_NO_EXISTE_CODIGO);

                var listaDetalleExiste = await _listaDetalleRepositorio.ObtenerPorListaIdYCodigoAsync(listaExiste.Id, listaDetalleCreacionRequest.Codigo);
                _listaDetalleValidador.ValidarDatoYaExiste(listaDetalleExiste, Textos.ListasDetalles.MENSAJE_LISTADETALLE_CODIGO_EXISTE);

                var listaDetalle = _mapper.Map<DCO_ListaDetalle>(listaDetalleCreacionRequest);
                listaDetalle.ListaId = listaExiste.Id;
                listaDetalle.UsuarioCreadorId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

                _listaDetalleRepositorio.MarcarCrear(listaDetalle);

                colas = await EncolarPublicacionActualizacion(listaExiste.Codigo);

                await _unidadDeTrabajo.GuardarCambiosAsync();

                id = listaDetalle.Id;
            });

            // Llamada para actualizar la sincronización de datos.
            await _sincronizadorMicroservicios.SincronizarTareasAsync(colas.Select(c => c.Id).ToList());

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        public async Task<ApiResponseDto<string>> ModificarAsync(ListaDetalleModificacionRequest listaDetalleModificacionRequest)
        {
            var colas = new List<DCO_ColaSolicitud>();
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async() => 
            {
                var listaDetalleExiste = await _listaDetalleRepositorio.ObtenerPorIdAsync(listaDetalleModificacionRequest.Id);
                _listaDetalleValidador.ValidarDatoNoEncontrado(listaDetalleExiste, Textos.ListasDetalles.MENSAJE_LISTADETALLE_NO_EXISTE_ID);

                _mapper.Map(listaDetalleModificacionRequest, listaDetalleExiste);
                listaDetalleExiste!.FechaModificado = DateTime.Now;
                listaDetalleExiste.UsuarioModificadorId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

                _listaDetalleRepositorio.MarcarModificar(listaDetalleExiste);

                var lista = await _listaRepositorio.ObtenerPorIdAsync(listaDetalleExiste.ListaId);
                colas = await EncolarPublicacionActualizacion(lista!.Codigo);

                await _unidadDeTrabajo.GuardarCambiosAsync();
            });

            // Llamada para actualizar la sincronización de datos.
            await _sincronizadorMicroservicios.SincronizarTareasAsync(colas.Select(c => c.Id).ToList());

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        public async Task<ApiResponseDto<string>> EliminarAsync(int id)
        {
            var colas = new List<DCO_ColaSolicitud>();
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var listaDetalleExiste = await _listaDetalleRepositorio.ObtenerPorIdAsync(id);
                _listaDetalleValidador.ValidarDatoNoEncontrado(listaDetalleExiste, Textos.ListasDetalles.MENSAJE_LISTADETALLE_NO_EXISTE_ID);

                _listaDetalleRepositorio.MarcarEliminar(listaDetalleExiste!);

                var lista = await _listaRepositorio.ObtenerPorIdAsync(listaDetalleExiste!.ListaId);
                colas = await EncolarPublicacionActualizacion(lista!.Codigo);

                await _unidadDeTrabajo.GuardarCambiosAsync();

            });

            // Llamada para actualizar la sincronización de datos.
            await _sincronizadorMicroservicios.SincronizarTareasAsync(colas.Select(c => c.Id).ToList());

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO, "");
        }

        public async Task<ApiResponseDto<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.ListarPorCodigoListaAsync(codigoLista);
            var listasDetallesDto = _mapper.Map<List<ListaDetalleDto>>(listasDetallesMV);

            return _apiResponse.CrearRespuesta<List<ListaDetalleDto>?>(true, "", listasDetallesDto);
        }

        public async Task<ApiResponseDto<List<ListaDetalleDto>?>> ListarPorCodigosListaAsync(List<string> codigosLista)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.ListarPorCodigosListaAsync(codigosLista);
            var listasDetallesDto = _mapper.Map<List<ListaDetalleDto>>(listasDetallesMV);
            return _apiResponse.CrearRespuesta<List<ListaDetalleDto>?>(true, "", listasDetallesDto);
        }

        public async Task<ApiResponseDto<List<ListaDetalleDto>?>> ListarPorCodigoConstanteAsync(string codigoConstante)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.ListarPorCodigoConstanteAsync(codigoConstante);
            var listasDetallesDto = _mapper.Map<List<ListaDetalleDto>>(listasDetallesMV);

            return _apiResponse.CrearRespuesta<List<ListaDetalleDto>?>(true, "", listasDetallesDto);
        }

        public async Task<ApiResponseDto<List<ListaDetalleDto>?>> ListarPorCodigosConstanteAsync(List<string> codigosConstante)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.ListarPorCodigosConstanteAsync(codigosConstante);
            var listasDetallesDto = _mapper.Map<List<ListaDetalleDto>>(listasDetallesMV);
            return _apiResponse.CrearRespuesta<List<ListaDetalleDto>?>(true, "", listasDetallesDto);
        }

        private async Task<List<DCO_ColaSolicitud>> EncolarPublicacionActualizacion(string codigoDetalle)
        {
            var codigo = new MaestroActualizadoEventoDto();
            codigo.CodigosMaestro.Add(codigoDetalle);
            var urls = _appSettings.ObtenerActualizarListasDetalleServicios();
            var colas = await _colaSolicitudServicio.AgregarColasSolicitudes(EventosColas.LISTASDETALLEACTUALIZADA, codigo, urls);
            return colas;
        }
    }
}
