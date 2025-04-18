using Microsoft.EntityFrameworkCore;
using AutoMapper;
using DCO.Dtos;
using DCO.Dominio.Entidades;
using Utilidades;
using DCO.Dominio.Repositorio;
using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.ServiciosExternos;
using System.Net.Http.Json;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class ListaServicio : IListaServicio
    {
        private readonly IListaRepositorio _listaRepositorio;
        private readonly IMapper _mapper;
        private readonly IMSSeguridadServicio _msSeguridadServicio;

        public ListaServicio(IListaRepositorio listaRepositorio, IMapper mapper, IMSSeguridadServicio msSeguridadServicio) 
        {
            _listaRepositorio = listaRepositorio;
            _mapper = mapper;
            _msSeguridadServicio = msSeguridadServicio;
        }

        public async Task<ApiResponse<int>> CrearAsync(ListaCreacionRequest listaCreacionRequest)
        {
            var listaExiste = await _listaRepositorio.ObtenerPorCodigoAsync(listaCreacionRequest.Codigo);
            if (listaExiste != null)
                throw new DbUpdateException(Textos.Listas.MENSAJE_LISTA_CODIGO_EXISTE);

            var respuesta = await _msSeguridadServicio.ObtenerNombreUsuarioPorIdAsync(listaCreacionRequest.UsuarioCreadorId);
            var usuarioAuditoriaExiste = await respuesta.Content.ReadFromJsonAsync<ApiResponse<string>>();
            if (!usuarioAuditoriaExiste.Correcto)
                throw new KeyNotFoundException(Textos.Usuarios.MENSAJE_USUARIO_AUDITORIA_NO_EXISTE_ID);

            var lista = _mapper.Map<DCO_Lista>(listaCreacionRequest);
            lista.FechaCreado = DateTime.Now;

            var id = await _listaRepositorio.CrearAsync(lista);

            return new ApiResponse<int> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_CREADO, Data = id };
        }

        public async Task<ApiResponse<string>> ModificarAsync(ListaModificacionRequest listaModificacionRequest)
        {
            var listaExiste = await _listaRepositorio.ObtenerPorIdAsync(listaModificacionRequest.Id);
            if (listaExiste == null)
                throw new KeyNotFoundException(Textos.Listas.MENSAJE_LISTA_NO_EXISTE_ID);

            var respuesta = await _msSeguridadServicio.ObtenerNombreUsuarioPorIdAsync(listaModificacionRequest.UsuarioModificadorId);
            var usuarioAuditoriaExiste = await respuesta.Content.ReadFromJsonAsync<ApiResponse<string>>();
            if (!usuarioAuditoriaExiste.Correcto)
                throw new KeyNotFoundException(Textos.Usuarios.MENSAJE_USUARIO_AUDITORIA_NO_EXISTE_ID);

            _mapper.Map(listaModificacionRequest, listaExiste);
            listaExiste.FechaModificado = DateTime.Now;

            await _listaRepositorio.ModificarAsync(listaExiste);

            return new ApiResponse<string> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO };
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id)
        {
            var listaExiste = await _listaRepositorio.ObtenerPorIdAsync(id);
            if (listaExiste == null)
                throw new KeyNotFoundException(Textos.Listas.MENSAJE_LISTA_NO_EXISTE_ID);

            var eliminado = await _listaRepositorio.EliminarAsync(id);

            if (eliminado)
                return new ApiResponse<string> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ELIMINADO };

            return new ApiResponse<string> { Correcto = false, Mensaje = Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO };
        }

        public async Task<ApiResponse<ListaDto?>> ObtenerPorIdAsync(int id)
        {
            var listaExiste = await _listaRepositorio.ObtenerPorIdAsync(id);
            if (listaExiste == null)
                throw new KeyNotFoundException(Textos.Listas.MENSAJE_LISTA_NO_EXISTE_ID);

            var listaDto = _mapper.Map<ListaDto>(listaExiste);

            return new ApiResponse<ListaDto?> { Correcto = true, Mensaje = "", Data = listaDto };
        }

        public async Task<ApiResponse<ListaDto?>> ObtenerPorCodigoAsync(string codigo)
        {
            var listaExiste = await _listaRepositorio.ObtenerPorCodigoAsync(codigo);
            if (listaExiste == null)
                throw new KeyNotFoundException(Textos.Listas.MENSAJE_LISTA_NO_EXISTE_CODIGO);

            var listaDto = _mapper.Map<ListaDto>(listaExiste);

            return new ApiResponse<ListaDto?> { Correcto = true, Mensaje = "", Data = listaDto };
        }

        public async Task<ApiResponse<List<ListaDto>?>> ListarAsync()
        {
            var listas = await _listaRepositorio.Listar().ToListAsync();
            var listasDto = _mapper.Map<List<ListaDto>>(listas);
            // Obtener los IDs únicos de los usuarios
            var usuarioIds = listasDto
                .SelectMany(lista => new[] { lista.UsuarioCreadorId, lista.UsuarioModificadorId })
                .Distinct()
                .ToList();

            // Consulta en lote al microservicio de seguridad
            var respuesta = await _msSeguridadServicio.ObtenerNombresUsuariosPorIds(usuarioIds);
            var nombresUsuarios = await respuesta.Content.ReadFromJsonAsync<ApiResponse<List<UsuarioDto>?>>();
            if (!nombresUsuarios.Correcto)
                throw new KeyNotFoundException("OJO CAMBIAR: NO FUE POSIBLE OBTENER LOS DATOS DEL MICROSERVICIO DE USUARIOS");

            // Crear un diccionario para facilitar la asignación
            var diccionarioUsuarios = nombresUsuarios?.Data?.ToDictionary(u => u.Id, u => u.NombreUsuario);

            // Asignar los nombres a los DTOs
            foreach (var lista in listasDto)
            {
                lista.NombreUsuarioCreador = diccionarioUsuarios?.GetValueOrDefault(lista.UsuarioCreadorId);
                if (lista.UsuarioModificadorId is not null)
                    lista.NombreUsuarioModificador = diccionarioUsuarios?.GetValueOrDefault((int)lista.UsuarioModificadorId);
            }

            return new ApiResponse<List<ListaDto>?> { Correcto = true, Mensaje = "", Data = listasDto };
        }
    }
}
    