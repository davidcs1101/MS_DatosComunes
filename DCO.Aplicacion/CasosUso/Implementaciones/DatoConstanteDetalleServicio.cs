using DCO.Dtos;
using DCO.Dominio.Entidades;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Utilidades;
using DCO.Dominio.Repositorio;
using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.ServiciosExternos;
using System.Net.Http.Json;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Dominio.Servicios.Interfaces;
using DCO.Dominio.Repositorio.UnidadTrabajo;
using DCO.Aplicacion.ServiciosExternos.config;
using DCO.Dominio.Enumeraciones;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class DatoConstanteDetalleServicio: IDatoConstanteDetalleServicio
    {
        private readonly IDatoConstanteRepositorio _datoConstanteRepositorio;
        private readonly IDatoConstanteDetalleRepositorio _datoConstanteDetalleRepositorio;
        private readonly IListaDetalleRepositorio _listaDetalleRepositorio;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IEntidadValidador<DCO_DatoConstante> _datoConstanteValidador;
        private readonly IApisResponse _apiResponse;
        private readonly IServicioComun _servicioComun;
        private readonly IEntidadValidador<DCO_ListaDetalle> _listaDetalleValidador;
        private readonly IEntidadValidador<DCO_DatoConstanteDetalle> _datoConstanteDetalleValidador;
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IConfiguracionesEventosNotificar _configuracionesEventosNotificar;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;
        private readonly IColaSolicitudRepositorio _colaSolicitudRepositorio;

        public DatoConstanteDetalleServicio(IDatoConstanteRepositorio datoConstanteRepositorio, IMapper mapper, IUsuarioContextoServicio usuarioContextoServicio, IMSSeguridad msSeguridad, IEntidadValidador<DCO_DatoConstante> datoConstanteValidador, IApisResponse apiResponseServicio, IServicioComun servicioComun, IDatoConstanteDetalleRepositorio datoConstanteDetalleRepositorio, IListaDetalleRepositorio listaDetalleRepositorio, IEntidadValidador<DCO_ListaDetalle> listaDetalleValidador, IEntidadValidador<DCO_DatoConstanteDetalle> datoConstanteDetalleValidador, IUnidadDeTrabajo unidadDeTrabajo, IConfiguracionesEventosNotificar configuracionesEventosNotificar, ISerializadorJsonServicio serializadorJsonServicio, IColaSolicitudRepositorio colaSolicitudRepositorio)
        {
            _datoConstanteRepositorio = datoConstanteRepositorio;
            _usuarioContextoServicio = usuarioContextoServicio;
            _datoConstanteValidador = datoConstanteValidador;
            _apiResponse = apiResponseServicio;
            _servicioComun = servicioComun;
            _datoConstanteDetalleRepositorio = datoConstanteDetalleRepositorio;
            _listaDetalleRepositorio = listaDetalleRepositorio;
            _listaDetalleValidador = listaDetalleValidador;
            _datoConstanteDetalleValidador = datoConstanteDetalleValidador;
            _unidadDeTrabajo = unidadDeTrabajo;
            _configuracionesEventosNotificar = configuracionesEventosNotificar;
            _serializadorJsonServicio = serializadorJsonServicio;
            _colaSolicitudRepositorio = colaSolicitudRepositorio;
        }

        public async Task<ApiResponse<int>> CrearAsync(DatoConstanteDetalleCreacionRequest datoConstanteDetalleCreacionRequest)
        {
            var id = 0;
            var colas = new List<DCO_ColaSolicitud>();
            await _servicioComun.EjecutarEnTransaccionAsync(async () =>
            {
                var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorCodigoAsync(datoConstanteDetalleCreacionRequest.CodigoConstante);
                _datoConstanteValidador.ValidarDatoNoEncontrado(datoConstanteExiste, Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_CODIGO);

                var listaDetalleExiste = await _listaDetalleRepositorio.ObtenerPorListaIdYCodigoAsync(
                    datoConstanteExiste.ListaId,datoConstanteDetalleCreacionRequest.CodigoListaDetalle);
                _listaDetalleValidador.ValidarDatoNoEncontrado(listaDetalleExiste, Textos.ListasDetalles.MENSAJE_LISTADETALLE_NO_EXISTE_CODIGO);

                var datoConstanteDetalleExiste = await _datoConstanteDetalleRepositorio.ObtenerPorDatoConstanteIdYListaDetalleIdAsync(
                    datoConstanteExiste.Id, listaDetalleExiste.Id);
                _datoConstanteDetalleValidador.ValidarDatoYaExiste(datoConstanteDetalleExiste, Textos.DatosConstantesDetalles.MENSAJE_DATOCONSTANTEDETALLE_LISTADETALLE_YA_EXISTE);

                var datoConstanteDetalle = new DCO_DatoConstanteDetalle();
                datoConstanteDetalle.DatoConstanteId = datoConstanteExiste.Id;
                datoConstanteDetalle.ListaDetalleId = listaDetalleExiste.Id;
                datoConstanteDetalle.FechaCreado = DateTime.Now;
                datoConstanteDetalle.UsuarioCreadorId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

                _datoConstanteDetalleRepositorio.MarcarCrear(datoConstanteDetalle);
                await _unidadDeTrabajo.GuardarCambiosAsync();

                var datosListasDetalle = await _servicioComun.ObtenerListasDetalleCodigoConstanteAsync(datoConstanteExiste.Codigo);

                var urls = _configuracionesEventosNotificar.ObtenerActualizarConstantesDetalleServicios();
                colas = this.AgregarColaSolicitud(datosListasDetalle, urls);

                await _unidadDeTrabajo.GuardarCambiosAsync();

                id = datoConstanteDetalle.Id;
            });

            _servicioComun.EncolarSolicitudes(colas);
            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        private List<DCO_ColaSolicitud> AgregarColaSolicitud(List<ListaDetalleDto> datosListasDetalle, List<string> urls)
        {
            var colas = new List<DCO_ColaSolicitud>();
            foreach (var url in urls)
            {
                var solicitud = new DCO_ColaSolicitud
                {
                    Tipo = Textos.EventosColas.CONSTANTESDETALLEACTUALIZADO,
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