using AutoMapper;
using DCO.Aplicacion.ServiciosExternos.config;
using DCO.Aplicacion.ServiciosExternos;
using DCO.Dominio.Repositorio.UnidadTrabajo;
using DCO.Dominio.Repositorio;
using DCO.Dominio.Excepciones;

namespace DCO.Aplicacion.Servicios.Interfaces
{
    public class ServicioComun : IServicioComun
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IJobEncoladorServicio _jobEncoladorServicio;
        private readonly IListaRepositorio _listaRepositorio;
        private readonly IListaDetalleRepositorio _listaDetalleRepositorio;
        private readonly IMapper _mapper;
        private readonly IConfiguracionesEventosNotificar _configuracionesEventosNotificar;

        public ServicioComun(IUnidadDeTrabajo unidadDeTrabajo, IJobEncoladorServicio jobEncoladorServicio,
                             IListaRepositorio listaRepositorio, IListaDetalleRepositorio listaDetalleRepositorio,
                             IMapper mapper, IConfiguracionesEventosNotificar configuracionesEventosNotificar)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _jobEncoladorServicio = jobEncoladorServicio;
            _listaRepositorio = listaRepositorio;
            _listaDetalleRepositorio = listaDetalleRepositorio;
            _mapper = mapper;
            _configuracionesEventosNotificar = configuracionesEventosNotificar;
        }

        public async Task EjecutarEnTransaccionAsync(Func<Task> operacion)
        {
            await using var transaccion = await _unidadDeTrabajo.IniciarTransaccionAsync();

            try
            {
                await operacion();
                await transaccion.CommitAsync();
            }
            catch (DatoNoEncontradoException)
            {
                await transaccion.RollbackAsync();
                throw;
            }
            catch (DatoYaExisteException)
            {
                await transaccion.RollbackAsync();
                throw;
            }
            catch
            {
                await transaccion.RollbackAsync();
                throw;
            }
        }
    }
}
