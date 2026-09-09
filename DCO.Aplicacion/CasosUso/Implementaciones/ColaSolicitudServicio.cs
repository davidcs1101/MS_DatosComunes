using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Aplicacion.ServiciosExternos;
using DCO.Aplicacion.ServiciosExternos.config;
using DCO.Dominio.Entidades;
using DCO.Dominio.Enumeraciones;
using DCO.Dominio.Repositorio;
using DCO.Dominio.Repositorio.UnidadTrabajo;
using DCO.Dominio.Servicios.Interfaces;
using Utilidades;
using Utilidades.Dtos;
using Utilidades.Servicios.Responses.Interfaces;
using Utilidades.Servicios.Serializacion.Interfaces;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class ColaSolicitudServicio : IColaSolicitudServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IColaSolicitudRepositorio _colaSolicitudRepositorio;
        private readonly IEntidadValidador<DCO_ColaSolicitud> _colaSolicitudValidador;
        private readonly IAppSettings _appSettings;
        private readonly IProcesadorTransacciones _procesadorTransacciones;
        private readonly IApiResponse _apiResponse;
        private readonly IProcesadorEventos _procesadorEventos;
        private readonly IJobEncoladorServicio _jobEncoladorServicio;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;

        public ColaSolicitudServicio(IUnidadDeTrabajo unidadTrabajo, IColaSolicitudRepositorio colaSolicitudRepositorio, 
            IEntidadValidador<DCO_ColaSolicitud> colaSolicitudValidador, IProcesadorEventos procesadorEventos, IAppSettings appSettings, 
            IProcesadorTransacciones procesadorTransacciones, IApiResponse apiResponse, IJobEncoladorServicio jobEncoladorServicio, 
            ISerializadorJsonServicio serializadorJsonServicio)
        {
            _unidadDeTrabajo = unidadTrabajo;
            _colaSolicitudRepositorio = colaSolicitudRepositorio;
            _colaSolicitudValidador = colaSolicitudValidador;
            _appSettings = appSettings;
            _procesadorTransacciones = procesadorTransacciones;
            _apiResponse = apiResponse;
            _procesadorEventos = procesadorEventos;
            _jobEncoladorServicio = jobEncoladorServicio;
            _serializadorJsonServicio = serializadorJsonServicio;
        }

        public async Task ProcesarColaSolicitudesAsync()
        {
            var cantidadRegistrosProcesar = _appSettings.ObtenerTrabajosColasSettings().CantidadRegistrosProcesarIteracion;
            var pendientes = await _colaSolicitudRepositorio.ListarAsync(EstadoCola.Pendiente, cantidadRegistrosProcesar);
            foreach (var solicitud in pendientes)
            {
                await this.ProcesarPorColaSolicitudIdAsync(solicitud.Id);
            }
        }

        public async Task ProcesarPorColaSolicitudIdAsync(int id, bool validarEstadoPendiente = false)
        {
            await _procesadorTransacciones.EjecutarEnTransaccionAsync(async () =>
            {
                var cantidadIntentos = _appSettings.ObtenerTrabajosColasSettings().CantidadIntentosPorRegistroEnCola;
                var solicitudExiste = await _colaSolicitudRepositorio.ObtenerPorIdAsync(id);
                _colaSolicitudValidador.ValidarDatoNoEncontrado(solicitudExiste, Textos.ColasSolicitudes.MENSAJE_COLASOLICITUD_NO_EXISTE_ID);

                if (validarEstadoPendiente)
                {
                    if (solicitudExiste!.Estado != EstadoCola.Pendiente)
                    {
                        Logs.EscribirLog("w", $"{Textos.ColasSolicitudes.MENSAJE_COLASOLICITUD_YA_PROCESADA}: {solicitudExiste.Id}");
                        return;
                    }
                }

                try
                {
                    solicitudExiste!.Estado = EstadoCola.Procesando;
                    solicitudExiste.FechaUltimoIntento = DateTime.Now;
                    _colaSolicitudRepositorio.MarcarModificar(solicitudExiste);
                    await _unidadDeTrabajo.GuardarCambiosAsync();

                    await _procesadorEventos.ProcesarAsync(solicitudExiste.Tipo, solicitudExiste.Payload, solicitudExiste.UrlDestino);

                    solicitudExiste.Estado = EstadoCola.Exitoso;
                    solicitudExiste.ErrorMensaje = null;
                }
                catch (Exception ex)
                {
                    solicitudExiste.Intentos++;
                    solicitudExiste.Estado = solicitudExiste.Intentos >= cantidadIntentos ? EstadoCola.Fallido : EstadoCola.Pendiente;
                    solicitudExiste.ErrorMensaje = ex.Message;
                    Logs.EscribirLog("e", $"{Textos.ColasSolicitudes.MENSAJE_COLASOLICITUD_ERROR_PROCESO} : {solicitudExiste.Id}", ex);
                }
                _colaSolicitudRepositorio.MarcarModificar(solicitudExiste);
                await _unidadDeTrabajo.GuardarCambiosAsync();
            });
        }

        public async Task<ApiResponseDto<int>> CrearAsync(ColaSolicitudCreacionRequest colaSolicitudCreacionRequest)
        {
            var solicitud = new DCO_ColaSolicitud
            {
                Tipo = colaSolicitudCreacionRequest.Tipo,
                Payload = colaSolicitudCreacionRequest.Payload,
                Estado = EstadoCola.Pendiente,
            };

            var id = await _colaSolicitudRepositorio.CrearAsync(solicitud);
            _ = _jobEncoladorServicio.EncolarPorColaSolicitudId(id, true);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        public async Task<DCO_ColaSolicitud> AgregarColaSolicitud(string tipo, object payload, string urlDestino = "")
        {
            return await AgregarCola(tipo, payload, urlDestino);
        }

        public async Task<List<DCO_ColaSolicitud>> AgregarColasSolicitudes(string tipo, object payload, List<string?>? urlsDestino = null)
        {
            if (urlsDestino is null || urlsDestino.Count == 0)
                return new List<DCO_ColaSolicitud>();

            var solicitudes = new List<DCO_ColaSolicitud>();
            foreach (var url in urlsDestino)
            {
                var cola = await AgregarCola(tipo, payload, url);
                solicitudes.Add(cola);
            }
            return solicitudes;
        }



        private async Task<DCO_ColaSolicitud> AgregarCola(string tipo, object payload, string urlDestino = "")
        {
            var solicitud = new DCO_ColaSolicitud
            {
                Tipo = tipo,
                UrlDestino = urlDestino,
                Payload = payload != null ? _serializadorJsonServicio.Serializar(payload) : string.Empty,
                Estado = EstadoCola.Pendiente,
            };
            _colaSolicitudRepositorio.MarcarCrear(solicitud);
            return solicitud;
        }
    }
}
