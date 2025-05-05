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
using static Utilidades.Textos;
using DCO.Dominio.Servicios.Interfaces;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class DatoConstanteServicio : IDatoConstanteServicio
    {
        private readonly IDatoConstanteRepositorio _datoConstanteRepositorio;
        private readonly IMapper _mapper;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly ISeguridadUsuarios _seguridadUsuarios;
        private readonly IDatoConstanteValidador _datoConstanteValidador;
        private readonly IApiResponse _apiResponse;

        public DatoConstanteServicio(IDatoConstanteRepositorio datoConstanteRepositorio, IMapper mapper, IUsuarioContextoServicio usuarioContextoServicio, ISeguridadUsuarios seguridadUsuarios, IDatoConstanteValidador datoConstanteValidador, IApiResponse apiResponseServicio)
        {
            _datoConstanteRepositorio = datoConstanteRepositorio;
            _mapper = mapper;
            _usuarioContextoServicio = usuarioContextoServicio;
            _seguridadUsuarios = seguridadUsuarios;
            _datoConstanteValidador = datoConstanteValidador;
            _apiResponse = apiResponseServicio;
        }

        public async Task<ApiResponse<int>> CrearAsync(DatoConstanteCreacionRequest datoConstanteCreacionRequest)
        {
            var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorCodigoAsync(datoConstanteCreacionRequest.Codigo);
            _datoConstanteValidador.ValidarDatoYaExiste(datoConstanteExiste, Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_CODIGO_EXISTE);

            var datoConstante = _mapper.Map<DCO_DatoConstante>(datoConstanteCreacionRequest);
            datoConstante.FechaCreado = DateTime.Now;
            datoConstante.UsuarioCreadorId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var id = await _datoConstanteRepositorio.CrearAsync(datoConstante);

            return new ApiResponse<int> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_CREADO, Data = id };
        }

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

        public async Task<ApiResponse<DatoConstanteDto?>> ObtenerPorIdAsync(int id)
        {
            var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorIdAsync(id);
            _datoConstanteValidador.ValidarDatoNoEncontrado(datoConstanteExiste, Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_ID);

            var datoConstanteDto = _mapper.Map<DatoConstanteDto>(datoConstanteExiste);

            return _apiResponse.CrearRespuesta<DatoConstanteDto?>(true, "", datoConstanteDto);
        }

        public async Task<ApiResponse<DatoConstanteDto?>> ObtenerPorCodigoAsync(string codigo)
        {
            var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorCodigoAsync(codigo);
            _datoConstanteValidador.ValidarDatoNoEncontrado(datoConstanteExiste, Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_CODIGO);

            var datoConstanteDto = _mapper.Map<DatoConstanteDto>(datoConstanteExiste);

            return _apiResponse.CrearRespuesta<DatoConstanteDto?>(true, "", datoConstanteDto);
        }

        public async Task<ApiResponse<List<DatoConstanteDto>?>> ListarAsync()
        {
            var datosConstantes = await _datoConstanteRepositorio.Listar().ToListAsync();
            var datosConstantesDto = _mapper.Map<List<DatoConstanteDto>>(datosConstantes);
            IdsListadoDto usuarioIds = new IdsListadoDto();

            // Obtener los IDs únicos de los usuarios
            usuarioIds.Ids = datosConstantesDto
                .SelectMany(datoConstante => new[] { datoConstante.UsuarioCreadorId, datoConstante.UsuarioModificadorId })
                .Distinct()
                .ToList();

            // Consulta en lote al microservicio de seguridad
            var nombresUsuarios = await _seguridadUsuarios.Listar(usuarioIds);

            // Crear un diccionario para facilitar la asignación
            var diccionarioUsuarios = nombresUsuarios?.Data?.ToDictionary(u => u.Id, u => u.NombreUsuario);

            // Asignar los nombres a los DTOs
            foreach (var datoConstante in datosConstantesDto)
            {
                datoConstante.NombreUsuarioCreador = diccionarioUsuarios?.GetValueOrDefault(datoConstante.UsuarioCreadorId);
                if (datoConstante.UsuarioModificadorId is not null)
                    datoConstante.NombreUsuarioModificador = diccionarioUsuarios?.GetValueOrDefault((int)datoConstante.UsuarioModificadorId);
            }

            var datoConstanteDto = _mapper.Map<List<DatoConstanteDto>>(datosConstantesDto);

            return _apiResponse.CrearRespuesta<List<DatoConstanteDto>?>(true, "", datoConstanteDto);
        }
    }
}    