using Utilidades.Dtos.Correos;
using Utilidades;
using Utilidades.Dtos;
using Utilidades.Servicios.Serializacion.Interfaces;
using Utilidades.Servicios.Http.Interfaces;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Aplicacion.Servicios.Interfaces.Cache;

namespace DCO.Aplicacion.Servicios.Implementaciones
{
    public class ProcesadorEventos : IProcesadorEventos
    {
        private readonly ISerializadorJsonServicio _serializadorJsonServicio;
        private readonly IPublicadorEventosBackgroundServicio _publicadorEventosBackgroundServicio;
        private readonly ISeguridadPermisosCache _seguridadPermisosCache;

        public ProcesadorEventos(ISerializadorJsonServicio serializadorJsonServicio,
            IPublicadorEventosBackgroundServicio publicadorEventosBackgroundServicio,
            ISeguridadPermisosCache seguridadPermisosCache)
        {
            _serializadorJsonServicio = serializadorJsonServicio;
            _publicadorEventosBackgroundServicio = publicadorEventosBackgroundServicio;
            _seguridadPermisosCache = seguridadPermisosCache;
        }

        public async Task ProcesarAsync(string evento, string payload = "", string UrlDestino = "")
        {
            switch (evento)
            {
                //Procesos publicación de eventos a otros microservicios
                case EventosColas.LISTASDETALLEACTUALIZADA:
                case EventosColas.CONSTANTESDETALLEACTUALIZADO:
                    await _publicadorEventosBackgroundServicio.PublicarActualizacion(UrlDestino, evento, payload);
                    break;

                //Procesos de actualización de catalógo caché de permisos local con datos de seguridad
                case EventosColas.PERMISOSACTUALIZADOS:
                    await _seguridadPermisosCache.RefrescarAsync();
                    break;
            }
        }

    }
}
