using DCO.Aplicacion.ServiciosExternos;
using DCO.Dominio.Repositorio.UnidadTrabajo;
using DCO.Dominio.Excepciones;
using DCO.Dominio.Entidades;
using DCO.Dominio.Repositorio;
using DCO.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DCO.Aplicacion.Servicios.Interfaces
{
    public class ProcesadorTransacciones : IProcesadorTransacciones
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IJobEncoladorServicio _jobEncoladorServicio;
        private readonly IListaDetalleRepositorio _listaDetalleRepositorio;
        private readonly IMapper _mapper;

        public ProcesadorTransacciones(IUnidadDeTrabajo unidadDeTrabajo, IJobEncoladorServicio jobEncoladorServicio, IListaDetalleRepositorio listaDetalleRepositorio, IMapper mapper)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _jobEncoladorServicio = jobEncoladorServicio;
            _listaDetalleRepositorio = listaDetalleRepositorio;
            _mapper = mapper;
        }

        public async Task EjecutarEnTransaccionAsync(Func<Task> operacion)
        {
            await using var transaccion = await _unidadDeTrabajo.IniciarTransaccionAsync();

            try
            {
                await operacion();
                await transaccion.CommitAsync();
            }
            catch (SolicitudHttpException){
                /*
                 *Tabmbien Hacemos RollBack si las solicitudes Http 
                 *fallan dado que puede ocurrir presencia de consumo a microservicios antes o despues de haber
                 *guardado cambios de base de datos.
                */
                await transaccion.RollbackAsync();
                throw;
            }
            catch (DatoNoEncontradoException){
                await transaccion.RollbackAsync();
                throw;
            }
            catch (DatoYaExisteException){
                await transaccion.RollbackAsync();
                throw;
            }
            catch{
                await transaccion.RollbackAsync();
                throw;
            }
        }

        public void EncolarSolicitudes(List<DCO_ColaSolicitud> listaColasSolicitudes) 
        {
            foreach (var cola in listaColasSolicitudes)
                _ = _jobEncoladorServicio.EncolarPorColaSolicitudId(cola.Id, true);
        }

        public async Task<List<ListaDetalleDto>?> ObtenerListasDetallePorCodigoListaAsync(string codigoLista)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.ListarPorCodigoLista(codigoLista).ToListAsync();
            var listasDetallesDto = _mapper.Map<List<ListaDetalleDto>>(listasDetallesMV);

            return listasDetallesDto;
        }

        public async Task<List<ListaDetalleDto>?> ObtenerListasDetalleCodigoConstanteAsync(string codigoConstante)
        {
            var listasDetallesMV = await _listaDetalleRepositorio.ListarPorCodigoConstante(codigoConstante).ToListAsync();
            var listasDetallesDto = _mapper.Map<List<ListaDetalleDto>>(listasDetallesMV);

            return listasDetallesDto;
        }
    }
}
