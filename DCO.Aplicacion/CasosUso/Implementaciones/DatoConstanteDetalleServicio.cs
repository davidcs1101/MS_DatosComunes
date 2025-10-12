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

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class DatoConstanteDetalleServicio
    {
        private readonly IDatoConstanteRepositorio _datoConstanteRepositorio;
        private readonly IDatoConstanteDetalleRepositorio _datoConstanteDetalleRepositorio;
        private readonly IMapper _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly IMSSeguridad _msSeguridad;
        private readonly IEntidadValidador<DCO_DatoConstante> _datoConstanteValidador;
        private readonly IApisResponse _apiResponse;
        private readonly IServicioComun _servicioComun;

        public DatoConstanteDetalleServicio(IDatoConstanteRepositorio datoConstanteRepositorio, IMapper mapper, IUsuarioContextoServicio usuarioContextoServicio, IMSSeguridad msSeguridad, IEntidadValidador<DCO_DatoConstante> datoConstanteValidador, IApisResponse apiResponseServicio, IServicioComun servicioComun, IDatoConstanteDetalleRepositorio datoConstanteDetalleRepositorio)
        {
            _datoConstanteRepositorio = datoConstanteRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
            _msSeguridad = msSeguridad;
            _datoConstanteValidador = datoConstanteValidador;
            _apiResponse = apiResponseServicio;
            _servicioComun = servicioComun;
            _datoConstanteDetalleRepositorio = datoConstanteDetalleRepositorio;
        }

        //public async Task<ApiResponse<int>> CrearAsync(DatoConstanteCreacionRequest datoConstanteCreacionRequest)
        //{
        //    var id = 0;
        //    var colas = new List<DCO_ColaSolicitud>();
        //    await _servicioComun.EjecutarEnTransaccionAsync(async () =>
        //    {
        //        var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorCodigoAsync(datoConstanteCreacionRequest.Codigo);
        //        _datoConstanteValidador.ValidarDatoNoEncontrado(datoConstanteExiste, Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_CODIGO);

        //        var datoConstante = _mapper.Map<DCO_DatoConstante>(datoConstanteCreacionRequest);
        //        datoConstante.FechaCreado = DateTime.Now;
        //        datoConstante.UsuarioCreadorId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

        //        _datoConstanteDetalleRepositorio.MarcarCrear(datoConstante);
        //        await _unidadDeTrabajo.GuardarCambiosAsync();
        //        var datosListasDetalle = await _listaDetalleServicio.ListarPorCodigoConstanteAsync(datoConstanteExiste.Codigo);

        //        var urls = _configuracionesEventosNotificar.ObtenerActualizarListasDetalleServicios();
        //        colas = this.AgregarColaSolicitud(datosListasDetalle.Data, urls);

        //        await _unidadDeTrabajo.GuardarCambiosAsync();

        //        id = listaDetalle.Id;
        //    });

        //    _servicioComun.EncolarSolicitudes(colas);
        //    return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);



        //    var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorCodigoAsync(datoConstanteCreacionRequest.Codigo);
        //    _datoConstanteValidador.ValidarDatoYaExiste(datoConstanteExiste, Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_CODIGO_EXISTE);

        //    var datoConstante = _mapper.Map<DCO_DatoConstante>(datoConstanteCreacionRequest);
        //    datoConstante.FechaCreado = DateTime.Now;
        //    datoConstante.UsuarioCreadorId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

        //    var id = await _datoConstanteRepositorio.CrearAsync(datoConstante);

        //    return new ApiResponse<int> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_CREADO, Data = id };
        //}

        public async Task<ApiResponse<string>> ModificarAsync(DatoConstanteModificacionRequest datoConstanteModificacionRequest)
        {
            var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorIdAsync(datoConstanteModificacionRequest.Id);
            _datoConstanteValidador.ValidarDatoNoEncontrado(datoConstanteExiste, Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_ID);

            _mapper.Map(datoConstanteModificacionRequest, datoConstanteExiste);
            datoConstanteExiste.FechaModificado = DateTime.Now;
            datoConstanteExiste.UsuarioModificadorId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            await _datoConstanteRepositorio.ModificarAsync(datoConstanteExiste);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id)
        {
            var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorIdAsync(id);
            _datoConstanteValidador.ValidarDatoNoEncontrado(datoConstanteExiste, Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_ID);

            var eliminado = await _datoConstanteRepositorio.EliminarAsync(id);

            if (eliminado)
                return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO, "");

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO, "");
        }

    }
}