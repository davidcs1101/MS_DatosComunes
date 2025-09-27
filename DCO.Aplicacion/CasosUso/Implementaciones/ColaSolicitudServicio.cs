using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Aplicacion.ServiciosExternos;
using DCO.Aplicacion.ServiciosExternos.config;
using DCO.Dominio.Entidades;
using DCO.Dominio.Enumeraciones;
using DCO.Dominio.Repositorio;
using DCO.Dominio.Repositorio.UnidadTrabajo;
using DCO.Dominio.Servicios.Interfaces;
using DCO.Dtos;
using Utilidades;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class ColaSolicitudServicio : IColaSolicitudServicio
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IColaSolicitudRepositorio _colaSolicitudRepositorio;
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;
        private readonly IEntidadValidador<DCO_ColaSolicitud> _colaSolicitudValidador;
        private readonly IConfiguracionesTrabajosColas _configuracionesTrabajosColas;
        private readonly IPublicadorEventosBackgroundServicio _publicadorEventosBackgroundServicio;

        public ColaSolicitudServicio(IUnidadDeTrabajo unidadTrabajo, IColaSolicitudRepositorio colaSolicitudRepositorio, ISerializadorJsonServicio serializadorJsonServicio, IEntidadValidador<DCO_ColaSolicitud> colaSolicitudValidador, IConfiguracionesTrabajosColas configuracionesTrabajosColas, IPublicadorEventosBackgroundServicio publicadorEventosBackgroundServicio)
        {
            _unidadDeTrabajo = unidadTrabajo;
            _colaSolicitudRepositorio = colaSolicitudRepositorio;
            _serializadorJsonServicio = serializadorJsonServicio;
            _colaSolicitudValidador = colaSolicitudValidador;
            _configuracionesTrabajosColas = configuracionesTrabajosColas;
            _publicadorEventosBackgroundServicio = publicadorEventosBackgroundServicio;
        }

        public async Task ProcesarColaSolicitudesAsync()
        {
            var pendientes = _colaSolicitudRepositorio.Listar().Where(c => c.Estado == EstadoCola.Pendiente).OrderBy(c => c.Id)
                .Take(_configuracionesTrabajosColas.ObtenerCantidadRegistrosProcesarIteracion()).ToList();

            foreach (var solicitud in pendientes)
            {
                await this.ProcesarPorColaSolicitudIdAsync(solicitud.Id);
            }
        }

        public async Task ProcesarPorColaSolicitudIdAsync(int id, bool validarEstadoPendiente = false)
        {
            await using var transaccion = await _unidadDeTrabajo.IniciarTransaccionAsync();

            var solicitudExiste = await _colaSolicitudRepositorio.ObtenerPorIdAsync(id);
            _colaSolicitudValidador.ValidarDatoNoEncontrado(solicitudExiste, Textos.ColasSolicitudes.MENSAJE_COLASOLICITUD_NO_EXISTE_ID);

            if (validarEstadoPendiente)
            {
                if (solicitudExiste.Estado != EstadoCola.Pendiente)
                {
                    Logs.EscribirLog("w", $"{Textos.ColasSolicitudes.MENSAJE_COLASOLICITUD_YA_PROCESADA}: {solicitudExiste.Id}");
                    return;
                }
            }

            try
            {
                solicitudExiste.Estado = EstadoCola.Procesando;
                solicitudExiste.FechaUltimoIntento = DateTime.Now;
                _colaSolicitudRepositorio.MarcarModificar(solicitudExiste);
                await _unidadDeTrabajo.GuardarCambiosAsync();

                await _publicadorEventosBackgroundServicio.PublicarActualizacionListaDetalle
                    (
                    solicitudExiste.UrlDestino,
                    _serializadorJsonServicio.Deserializar<List<ListaDetalleDto>>(solicitudExiste.Payload)
                    );

                solicitudExiste.Estado = EstadoCola.Exitoso;
                solicitudExiste.ErrorMensaje = null;
            }
            catch (Exception ex)
            {
                solicitudExiste.Intentos++;
                solicitudExiste.Estado = solicitudExiste.Intentos >= _configuracionesTrabajosColas.ObtenerCantidadIntentosPorRegistroEnCola() ? EstadoCola.Fallido : EstadoCola.Pendiente;
                solicitudExiste.ErrorMensaje = ex.Message;
                Logs.EscribirLog("e", $"{Textos.ColasSolicitudes.MENSAJE_COLASOLICITUD_ERROR_PROCESO} : {solicitudExiste.Id}", ex);
            }
            _colaSolicitudRepositorio.MarcarModificar(solicitudExiste);
            await _unidadDeTrabajo.GuardarCambiosAsync();
            await transaccion.CommitAsync();
        }
    }
}
