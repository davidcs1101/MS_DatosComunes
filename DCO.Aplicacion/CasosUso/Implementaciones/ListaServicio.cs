using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DCO.Dtos;
using DCO.Dominio.Entidades;
using Utilidades;
using DCO.Dominio.Repositorio;
using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.ServiciosExternos;
using System.Net.Http.Json;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Aplicacion.Servicios.Implementaciones;
using DCO.Dominio.Servicios.Interfaces;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class ListaServicio : IListaServicio
    {
        private readonly IListaRepositorio _listaRepositorio;
        private readonly IMapper _mapper;
        private readonly IListaValidador _listaValidador;
        private readonly IApiResponse _apiResponse;
        private readonly IUsuarioContextoServicio _usuarioContextoServicio;
        private readonly ISeguridadUsuarios _seguridadUsuarios;

        public ListaServicio(IListaRepositorio listaRepositorio, IMapper mapper, IListaValidador listaValidador = null, IApiResponse apiResponseServicio = null, IUsuarioContextoServicio usuarioContextoServicio = null, ISeguridadUsuarios seguridadUsuarios = null)
        {
            _listaRepositorio = listaRepositorio;
            _mapper = mapper;
            _listaValidador = listaValidador;
            _apiResponse = apiResponseServicio;
            _usuarioContextoServicio = usuarioContextoServicio;
            _seguridadUsuarios = seguridadUsuarios;
        }

        public async Task<ApiResponse<int>> CrearAsync(ListaCreacionRequest listaCreacionRequest)
        {
            var listaExiste = await _listaRepositorio.ObtenerPorCodigoAsync(listaCreacionRequest.Codigo);
            _listaValidador.ValidarDatoYaExiste(listaExiste, Textos.Listas.MENSAJE_LISTA_CODIGO_EXISTE);

            var lista = _mapper.Map<DCO_Lista>(listaCreacionRequest);
            lista.FechaCreado = DateTime.Now;
            lista.UsuarioCreadorId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            var id = await _listaRepositorio.CrearAsync(lista);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_CREADO, id);
        }

        public async Task<ApiResponse<string>> ModificarAsync(ListaModificacionRequest listaModificacionRequest)
        {
            var listaExiste = await _listaRepositorio.ObtenerPorIdAsync(listaModificacionRequest.Id);
            _listaValidador.ValidarDatoNoEncontrado(listaExiste, Textos.Listas.MENSAJE_LISTA_NO_EXISTE_ID);

            _mapper.Map(listaModificacionRequest, listaExiste);
            listaExiste.FechaModificado = DateTime.Now;
            listaExiste.UsuarioModificadorId = _usuarioContextoServicio.ObtenerUsuarioIdToken();

            await _listaRepositorio.ModificarAsync(listaExiste);

            return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO, "");
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id)
        {
            var listaExiste = await _listaRepositorio.ObtenerPorIdAsync(id);
            _listaValidador.ValidarDatoNoEncontrado(listaExiste, Textos.Listas.MENSAJE_LISTA_NO_EXISTE_ID);

            var eliminado = await _listaRepositorio.EliminarAsync(id);

            if (eliminado)
                return _apiResponse.CrearRespuesta(true, Textos.Generales.MENSAJE_REGISTRO_ELIMINADO, "");

            return _apiResponse.CrearRespuesta(false, Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO, "");
        }

        public async Task<ApiResponse<ListaDto?>> ObtenerPorIdAsync(int id)
        {
            var listaExiste = await _listaRepositorio.ObtenerPorIdAsync(id);
            _listaValidador.ValidarDatoNoEncontrado(listaExiste, Textos.Listas.MENSAJE_LISTA_NO_EXISTE_ID);

            var listaDto = _mapper.Map<ListaDto>(listaExiste);

            return _apiResponse.CrearRespuesta<ListaDto?>(true, "", listaDto);
        }

        public async Task<ApiResponse<ListaDto?>> ObtenerPorCodigoAsync(string codigo)
        {
            var listaExiste = await _listaRepositorio.ObtenerPorCodigoAsync(codigo);
            _listaValidador.ValidarDatoNoEncontrado(listaExiste, Textos.Listas.MENSAJE_LISTA_NO_EXISTE_CODIGO);

            var listaDto = _mapper.Map<ListaDto>(listaExiste);

            return _apiResponse.CrearRespuesta<ListaDto?>(true, "", listaDto);
        }

        public async Task<ApiResponse<List<ListaDto>?>> ListarAsync()
        {
            var listas = await _listaRepositorio.Listar().ToListAsync();
            var listasDto = _mapper.Map<List<ListaDto>>(listas);
            IdsListadoDto usuarioIds = new IdsListadoDto();

            // Obtener los IDs únicos de los usuarios
            usuarioIds.Ids = listasDto
                .SelectMany(lista => new[] { lista.UsuarioCreadorId, lista.UsuarioModificadorId })
                .Distinct()
                .ToList();

            // Consulta en lote al microservicio de seguridad
            var nombresUsuarios = await _seguridadUsuarios.Listar(usuarioIds);

            // Crear un diccionario para facilitar la asignación
            var diccionarioUsuarios = nombresUsuarios?.Data?.ToDictionary(u => u.Id, u => u.NombreUsuario);

            // Asignar los nombres a los DTOs
            foreach (var lista in listasDto)
            {
                lista.NombreUsuarioCreador = diccionarioUsuarios?.GetValueOrDefault(lista.UsuarioCreadorId);
                if (lista.UsuarioModificadorId is not null)
                    lista.NombreUsuarioModificador = diccionarioUsuarios?.GetValueOrDefault((int)lista.UsuarioModificadorId);
            }

            return _apiResponse.CrearRespuesta<List<ListaDto>?>(true, "", listasDto);
        }
    }
}
    