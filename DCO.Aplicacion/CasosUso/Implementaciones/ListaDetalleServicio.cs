using DCO.Dtos;
using Microsoft.EntityFrameworkCore;
using DCO.Dominio.Repositorio;
using DCO.Aplicacion.CasosUso.Interfaces;
using AutoMapper;
using DCO.Dominio.Entidades.ModelosVistas;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Dominio.Servicios.Interfaces;
using Utilidades;
using DCO.Dominio.Entidades;
using DCO.Aplicacion.ServiciosExternos;
using DCO.Dominio.Repositorio.UnidadTrabajo;
using DCO.Dominio.Enumeraciones;
using DCO.Aplicacion.ServiciosExternos.config;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class ListaDetalleServicio : IListaDetalleServicio
    {

        private readonly IMapper _mapper;
        private readonly IApisResponse _apiResponse;
        private readonly IListaRepositorio _listaRepositorio;
        private readonly IEntidadValidador<DCO_Lista> _listaValidador;
        private readonly IListaDetalleRepositorio _listaDetalleRepositorio;
        private readonly IEntidadValidador<ListaDetalleMV> _listaDetalleMVValidador;
        private readonly IEntidadValidador<DCO_ListaDetalle> _listaDetalleValidador;
        private readonly IDatoConstanteRepositorio _datoConstanteRepositorio;
        private readonly IEntidadValidador<DCO_DatoConstante> _datoConstanteValidador;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;
        private readonly IColaSolicitudRepositorio _colaSolicitudRepositorio;
        private readonly IConfiguracionesEventosNotificar _configuracionesEventosNotificar;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IServicioComun _servicioComun;

        public ListaDetalleServicio(IListaDetalleRepositorio listaDetalleRepositorio, IMapper mapper, IApisResponse apiResponseServicio, IEntidadValidador<ListaDetalleMV> entidadValidador, IEntidadValidador<ListaDetalleMV> listaDetalleMVValidador, IEntidadValidador<DCO_DatoConstante> datoConstanteValidador, IDatoConstanteRepositorio datoConstanteRepositorio, IListaRepositorio listaRepositorio, IEntidadValidador<DCO_Lista> listaValidador, IUnidadDeTrabajo unidadDeTrabajo, ISerializadorJsonServicio serializadorJsonServicio, IColaSolicitudRepositorio colaSolicitudRepositorio, IConfiguracionesEventosNotificar configuracionesEventosNotificar, IUsuarioContextoServicio usuarioContextoServicio, IEntidadValidador<DCO_ListaDetalle> listaDetalleValidador, IServicioComun servicioComun)
        {
            _listaDetalleRepositorio = listaDetalleRepositorio;
            _mapper = mapper;
            _apiResponse = apiResponseServicio;
            _listaDetalleMVValidador = listaDetalleMVValidador;
            _datoConstanteValidador = datoConstanteValidador;
            _datoConstanteRepositorio = datoConstanteRepositorio;
            _listaRepositorio = listaRepositorio;
            _listaValidador = listaValidador;
            _unidadDeTrabajo = unidadDeTrabajo;
            _serializadorJsonServicio = serializadorJsonServicio;
            _colaSolicitudRepositorio = colaSolicitudRepositorio;
            _configuracionesEventosNotificar = configuracionesEventosNotificar;
            _usuarioContextoServicio = usuarioContextoServicio;
            _listaDetalleValidador = listaDetalleValidador;
            _servicioComun = servicioComun;
        }

        public async Task<ApiResponse<int>> CrearAsync(ListaDetalleCreacionRequest listaDetalleCreacionRequest)
        {
            var id = 0;
            var colas = new List<DCO_ColaSolicitud>();
            await _servicioComun.EjecutarEnTransaccionAsync(async () =>
            {
                var listaExiste = await _listaRepositorio.ObtenerPorCodigoAsync(listaDetalleCreacionRequest.CodigoLista);
                _listaValidador.ValidarDatoNoEncontrado(listaExiste, Textos.Listas.MENSAJE_LISTA_NO_EXISTE_CODIGO);

                var listaDetalleExiste = await _listaDetalleRepositorio.ObtenerPorListaIdYCodigoAsync(listaExiste.Id, listaDetalleCreacionRequest.Codigo);
                _listaDetalleValidador.ValidarDatoYaExiste(listaDetalleExiste, Textos.ListasDetalles.MENSAJE_LISTADETALLE_CODIGO_EXISTE);

                var listaDetalle = _mapper.Map<DCO_ListaDetalle>(listaDetalleCreacionRequest);
                listaDetalle.ListaId = listaExiste.Id;
                listaDetalle.UsuarioCreadorId = 1;// _usuarioContextoServicio.ObtenerUsuarioIdToken();

                _listaDetalleRepositorio.MarcarCrear(listaDetalle);
                await _unidadDeTrabajo.GuardarCambiosAsync();

                var datosListasDetalle = await this.ListarPorCodigoListaAsync(listaExiste.Codigo);

                var urls = _configuracionesEventosNotificar.ObtenerActualizarListasDetalleServicios();
                colas = this.AgregarColaSolicitud(datosListasDetalle.Data, urls);

                await _unidadDeTrabajo.GuardarCambiosAsync();

                id = listaDetalle.Id;
            });

            _servicioComun.EncolarSolicitudes(colas);
            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        public async Task<ApiResponse<string>> ModificarAsync(ListaDetalleModificacionRequest listaDetalleModificacionRequest)
        {
            var colas = new List<DCO_ColaSolicitud>();
            await _servicioComun.EjecutarEnTransaccionAsync(async() => 
            {
                var listaDetalleExiste = await _listaDetalleRepositorio.ObtenerPorIdAsync(listaDetalleModificacionRequest.Id);
                _listaDetalleValidador.ValidarDatoNoEncontrado(listaDetalleExiste, Textos.ListasDetalles.MENSAJE_LISTADETALLE_NO_EXISTE_ID);

                _mapper.Map(listaDetalleModificacionRequest, listaDetalleExiste);
                listaDetalleExiste.FechaModificado = DateTime.Now;
                listaDetalleExiste.UsuarioModificadorId = 1;// _usuarioContextoServicio.ObtenerUsuarioIdToken();

                _listaDetalleRepositorio.MarcarModificar(listaDetalleExiste);
                await _unidadDeTrabajo.GuardarCambiosAsync();

                var lista = await _listaRepositorio.ObtenerPorIdAsync(listaDetalleExiste.ListaId);
                var datosListasDetalle = await this.ListarPorCodigoListaAsync(lista.Codigo);

                var urls = _configuracionesEventosNotificar.ObtenerActualizarListasDetalleServicios();
                colas = this.AgregarColaSolicitud(datosListasDetalle.Data, urls);

                await _unidadDeTrabajo.GuardarCambiosAsync();
            });

            _servicioComun.EncolarSolicitudes(colas);
            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id)
        {
            var colas = new List<DCO_ColaSolicitud>();
            await _servicioComun.EjecutarEnTransaccionAsync(async () =>
            {
                var listaDetalleExiste = await _listaDetalleRepositorio.ObtenerPorIdAsync(id);
                _listaDetalleValidador.ValidarDatoNoEncontrado(listaDetalleExiste, Textos.ListasDetalles.MENSAJE_LISTADETALLE_NO_EXISTE_ID);

                var listaId = listaDetalleExiste.ListaId;

                _listaDetalleRepositorio.MarcarEliminar(listaDetalleExiste);
                await _unidadDeTrabajo.GuardarCambiosAsync();

                var lista = await _listaRepositorio.ObtenerPorIdAsync(listaId);
                var datosListasDetalle = await this.ListarPorCodigoListaAsync(lista.Codigo);

                var urls = _configuracionesEventosNotificar.ObtenerActualizarListasDetalleServicios();
                var colas = this.AgregarColaSolicitud(datosListasDetalle.Data, urls);

                await _unidadDeTrabajo.GuardarCambiosAsync();

            });

            _servicioComun.EncolarSolicitudes(colas);
            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO, "");
        }

        public async Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.ListarPorCodigoLista(codigoLista).ToListAsync();
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

            _listaDetalleMVValidador.ValidarDatoNoEncontrado(listasDetallesMV, Textos.ListasDetalles.MENSAJE_LISTADETALLE_NO_EXISTE_EN_CODIGOLISTA(codigoDetalleRequest.Codigo, codigoDetalleRequest.CodigoListaDetalle));
            var listasDetallesDto = _mapper.Map<ListaDetalleDto>(listasDetallesMV);

            return _apiResponse.CrearRespuesta<ListaDetalleDto?>(true, "", listasDetallesDto);
        }

        public async Task<ApiResponse<ListaDetalleDto?>> ObtenerPorCodigoConstanteYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest) 
        {
            var datoConstante = await _datoConstanteRepositorio.ObtenerPorCodigoAsync(codigoDetalleRequest.Codigo);
            _datoConstanteValidador.ValidarDatoNoEncontrado(datoConstante, Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_CODIGO);

            var listasDetallesMV = await _listaDetalleRepositorio.ListarPorCodigoConstante(codigoDetalleRequest.Codigo)
                .Where(ld => ld.Codigo == codigoDetalleRequest.CodigoListaDetalle).FirstOrDefaultAsync();

            _listaDetalleMVValidador.ValidarDatoNoEncontrado(listasDetallesMV, Textos.ListasDetalles.MENSAJE_LISTADETALLE_NO_EXISTE_EN_CODIGOCONSTANTE(codigoDetalleRequest.Codigo, codigoDetalleRequest.CodigoListaDetalle));
            var listasDetallesDto = _mapper.Map<ListaDetalleDto>(listasDetallesMV);

            return _apiResponse.CrearRespuesta<ListaDetalleDto?>(true, "", listasDetallesDto);
        }

        private List<DCO_ColaSolicitud> AgregarColaSolicitud(List<ListaDetalleDto> datosListasDetalle, List<string> urls)
        {
            var colas = new List<DCO_ColaSolicitud>();
            foreach (var url in urls)
            {
                var solicitud = new DCO_ColaSolicitud
                {
                    Tipo = Textos.EventosColas.LISTASDETALLEACTUALIZADA,
                    UrlDestino = url,
                    Payload = _serializadorJsonServicio.Serializar(datosListasDetalle),
                    Estado = EstadoCola.Pendiente,
                    Intentos = 0,
                    FechaCreado = DateTime.Now
                };
                _colaSolicitudRepositorio.MarcarCrear(solicitud);
                colas.Add(solicitud);
            }
            return colas;
        }
    }
}
