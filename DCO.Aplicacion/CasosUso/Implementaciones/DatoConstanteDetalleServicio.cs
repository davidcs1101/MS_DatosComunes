using DCO.Dtos;
using DCO.Dominio.Entidades;
using AutoMapper;
using Utilidades;
using DCO.Dominio.Repositorio;
using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Dominio.Servicios.Interfaces;
using DCO.Dominio.Repositorio.UnidadTrabajo;
using DCO.Aplicacion.ServiciosExternos.config;
using Utilidades.Dtos;
using Utilidades.Servicios.Responses.Interfaces;
using Utilidades.Servicios.Http.Interfaces;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class DatoConstanteDetalleServicio: IDatoConstanteDetalleServicio
    {
        private readonly IDatoConstanteRepositorio _datoConstanteRepositorio;
        private readonly IDatoConstanteDetalleRepositorio _datoConstanteDetalleRepositorio;
        private readonly IListaDetalleRepositorio _listaDetalleRepositorio;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IEntidadValidador<DCO_DatoConstante> _datoConstanteValidador;
        private readonly IApiResponse _apiResponse;
        private readonly IProcesadorTransacciones _procesadorTransacciones;
        private readonly IEntidadValidador<DCO_ListaDetalle> _listaDetalleValidador;
        private readonly IEntidadValidador<DCO_DatoConstanteDetalle> _datoConstanteDetalleValidador;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IAppSettings _appSettings;
        private readonly IColaSolicitudServicio _colaSolicitudServicio;
        private readonly ISincronizadorMicroservicios _sincronizadorMicroservicios;

        public DatoConstanteDetalleServicio(IDatoConstanteRepositorio datoConstanteRepositorio, IMapper mapper, IUsuarioContextoServicio usuarioContextoServicio, IEntidadValidador<DCO_DatoConstante> datoConstanteValidador, IApiResponse apiResponseServicio, IProcesadorTransacciones procesadorTransacciones, IDatoConstanteDetalleRepositorio datoConstanteDetalleRepositorio, IListaDetalleRepositorio listaDetalleRepositorio, IEntidadValidador<DCO_ListaDetalle> listaDetalleValidador, IEntidadValidador<DCO_DatoConstanteDetalle> datoConstanteDetalleValidador, IUnidadDeTrabajo unidadDeTrabajo,
            IAppSettings appSettings, IColaSolicitudServicio colaSolicitudServicio, ISincronizadorMicroservicios sincronizadorMicroservicios)
        {
            _datoConstanteRepositorio = datoConstanteRepositorio;
            _usuarioContextoServicio = usuarioContextoServicio;
            _datoConstanteValidador = datoConstanteValidador;
            _apiResponse = apiResponseServicio;
            _procesadorTransacciones = procesadorTransacciones;
            _datoConstanteDetalleRepositorio = datoConstanteDetalleRepositorio;
            _listaDetalleRepositorio = listaDetalleRepositorio;
            _listaDetalleValidador = listaDetalleValidador;
            _datoConstanteDetalleValidador = datoConstanteDetalleValidador;
            _unidadDeTrabajo = unidadDeTrabajo;
            _appSettings = appSettings;
            _colaSolicitudServicio = colaSolicitudServicio;
            _sincronizadorMicroservicios = sincronizadorMicroservicios;
        }

        public async Task<ApiResponseDto<int>> CrearAsync(DatoConstanteDetalleCreacionRequest datoConstanteDetalleCreacionRequest)
        {
            var id = 0;
            var colas = new List<DCO_ColaSolicitud>();
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorCodigoAsync(datoConstanteDetalleCreacionRequest.CodigoConstante);
                _datoConstanteValidador.ValidarDatoNoEncontrado(datoConstanteExiste, Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_CODIGO);

                var listaDetalleExiste = await _listaDetalleRepositorio.ObtenerPorListaIdYCodigoAsync(
                    datoConstanteExiste!.ListaId,datoConstanteDetalleCreacionRequest.CodigoListaDetalle);
                _listaDetalleValidador.ValidarDatoNoEncontrado(listaDetalleExiste, Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_LISTA_NO_EXISTE_CODIGO);

                var datoConstanteDetalleExiste = await _datoConstanteDetalleRepositorio.ObtenerPorDatoConstanteIdYListaDetalleIdAsync(
                    datoConstanteExiste.Id, listaDetalleExiste!.Id);
                _datoConstanteDetalleValidador.ValidarDatoYaExiste(datoConstanteDetalleExiste, Textos.DatosConstantesDetalles.MENSAJE_DATOCONSTANTEDETALLE_LISTADETALLE_YA_EXISTE);

                var datoConstanteDetalle = new DCO_DatoConstanteDetalle();
                datoConstanteDetalle.DatoConstanteId = datoConstanteExiste.Id;
                datoConstanteDetalle.ListaDetalleId = listaDetalleExiste.Id;
                datoConstanteDetalle.FechaCreado = DateTime.Now;
                datoConstanteDetalle.UsuarioCreadorId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

                _datoConstanteDetalleRepositorio.MarcarCrear(datoConstanteDetalle);

                colas = await EncolarPublicacionActualizacion(datoConstanteExiste.Codigo);

                await _unidadDeTrabajo.GuardarCambiosAsync();

                id = datoConstanteDetalle.Id;
            });

            // Llamada para actualizar la sincronización de datos.
            await _sincronizadorMicroservicios.SincronizarTareasAsync(colas.Select(c => c.Id).ToList());

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        public async Task<ApiResponseDto<string>> ModificarAsync(DatoConstanteDetalleModificacionRequest datoConstanteDetalleModificacionRequest)
        {
            var colas = new List<DCO_ColaSolicitud>();
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var datoConstanteDetalleExiste = await _datoConstanteDetalleRepositorio.ObtenerPorId(datoConstanteDetalleModificacionRequest.Id);
                _datoConstanteDetalleValidador.ValidarDatoNoEncontrado(datoConstanteDetalleExiste, Textos.DatosConstantesDetalles.MENSAJE_DATOCONSTANTEDETALLE_LISTADETALLE_NO_EXISTE_ID);

                datoConstanteDetalleExiste!.FechaModificado = DateTime.Now;
                datoConstanteDetalleExiste.UsuarioModificadorId = _usuarioContextoServicio.ObtenerUsuarioIdToken();
                datoConstanteDetalleExiste.EstadoActivo = datoConstanteDetalleModificacionRequest.EstadoActivo;

                _datoConstanteDetalleRepositorio.MarcarModificar(datoConstanteDetalleExiste);

                colas = await EncolarPublicacionActualizacion(datoConstanteDetalleExiste.DatoConstante.Codigo);

                await _unidadDeTrabajo.GuardarCambiosAsync();
            });

            // Llamada para actualizar la sincronización de datos.
            await _sincronizadorMicroservicios.SincronizarTareasAsync(colas.Select(c => c.Id).ToList());

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        private async Task<List<DCO_ColaSolicitud>> EncolarPublicacionActualizacion(string codigoDetalle)
        {
            var codigo = new MaestroActualizadoEventoDto();
            codigo.CodigosMaestro.Add(codigoDetalle);
            var urls = _appSettings.ObtenerActualizarConstantesDetalleServicios();
            var colas = await _colaSolicitudServicio.AgregarColasSolicitudes(EventosColas.CONSTANTESDETALLEACTUALIZADO, codigo, urls);
            return colas;
        }

    }
}