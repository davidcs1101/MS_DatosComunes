﻿using DCO.Dtos;
using DCO.Servicio.Interfaces;
using DCO.Dominio.Entidades;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCO.Repositorio.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Utilidades;

namespace DCO.Servicio.Implementaciones
{
    public class DatoConstanteServicio : IDatoConstanteServicio
    {
        private readonly IDatoConstanteRepositorio _datoConstanteRepositorio;
        private readonly IMapper _mapper;
        private readonly IMSSeguridadServicio _msSeguridadServicio;

        public DatoConstanteServicio(IDatoConstanteRepositorio datoConstanteRepositorio, IMapper mapper, IMSSeguridadServicio msSeguridadServicio) 
        {
            _datoConstanteRepositorio = datoConstanteRepositorio;
            _mapper = mapper;
            _msSeguridadServicio = msSeguridadServicio;
        }

        public async Task<ApiResponse<int>> CrearAsync(DatoConstanteCreacionRequest datoConstanteCreacionRequest)
        {
            var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorCodigoAsync(datoConstanteCreacionRequest.Codigo);
            if (datoConstanteExiste != null)
                throw new DbUpdateException(Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_CODIGO_EXISTE);

            var usuarioAuditoriaExiste = await _msSeguridadServicio.ObtenerNombreUsuarioPorIdAsync(datoConstanteCreacionRequest.UsuarioCreadorId);
            if (!usuarioAuditoriaExiste.Correcto)
                throw new KeyNotFoundException(Textos.Usuarios.MENSAJE_USUARIO_AUDITORIA_NO_EXISTE_ID);

            var datoConstante = _mapper.Map<DCO_DatoConstante>(datoConstanteCreacionRequest);
            datoConstante.FechaCreado = DateTime.Now;

            var id = await _datoConstanteRepositorio.CrearAsync(datoConstante);

            return new ApiResponse<int> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_CREADO, Data = id };
        }

        public async Task<ApiResponse<string>> ModificarAsync(DatoConstanteModificacionRequest datoConstanteModificacionRequest)
        {
            var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorIdAsync(datoConstanteModificacionRequest.Id);
            if (datoConstanteExiste == null)
                throw new KeyNotFoundException(Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_ID);

            var usuarioAuditoriaExiste = await _msSeguridadServicio.ObtenerNombreUsuarioPorIdAsync(datoConstanteModificacionRequest.UsuarioModificadorId);
            if (!usuarioAuditoriaExiste.Correcto)
                throw new KeyNotFoundException(Textos.Usuarios.MENSAJE_USUARIO_AUDITORIA_NO_EXISTE_ID);

            _mapper.Map(datoConstanteModificacionRequest, datoConstanteExiste);
            datoConstanteExiste.FechaModificado = DateTime.Now;

            await _datoConstanteRepositorio.ModificarAsync(datoConstanteExiste);

            return new ApiResponse<string> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ACTUALIZADO };
        }

        public async Task<ApiResponse<string>> EliminarAsync(int id)
        {
            var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorIdAsync(id);
            if (datoConstanteExiste == null)
                throw new KeyNotFoundException(Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_ID);

            var eliminado = await _datoConstanteRepositorio.EliminarAsync(id);

            if (eliminado)
                return new ApiResponse<string> { Correcto = true, Mensaje = Textos.Generales.MENSAJE_REGISTRO_ELIMINADO };

            return new ApiResponse<string> { Correcto = false, Mensaje = Textos.Generales.MENSAJE_REGISTRO_NO_ELIMINADO };
        }

        public async Task<ApiResponse<DatoConstanteDto?>> ObtenerPorIdAsync(int id)
        {
            var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorIdAsync(id);
            if (datoConstanteExiste == null)
                throw new KeyNotFoundException(Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_ID);

            var datoConstanteDto = _mapper.Map<DatoConstanteDto>(datoConstanteExiste);

            return new ApiResponse<DatoConstanteDto?> { Correcto = true, Mensaje = "", Data = datoConstanteDto };
        }

        public async Task<ApiResponse<DatoConstanteDto?>> ObtenerPorCodigoAsync(string codigo)
        {
            var datoConstanteExiste = await _datoConstanteRepositorio.ObtenerPorCodigoAsync(codigo);
            if (datoConstanteExiste == null)
                throw new KeyNotFoundException(Textos.DatosConstantes.MENSAJE_DATOCONSTANTE_NO_EXISTE_CODIGO);

            var datoConstanteDto = _mapper.Map<DatoConstanteDto>(datoConstanteExiste);

            return new ApiResponse<DatoConstanteDto?> { Correcto = true, Mensaje = "", Data = datoConstanteDto };
        }

        public async Task<ApiResponse<List<DatoConstanteDto>?>> ListarAsync()
        {
            var datosConstantesResultado = await _datoConstanteRepositorio.Listar().ToListAsync();

            // Obtener los IDs únicos de los usuarios
            var usuarioIds = datosConstantesResultado
                .SelectMany(datoConstante => new[] { datoConstante.UsuarioCreadorId, datoConstante.UsuarioModificadorId })
                .Distinct()
                .ToList();

            // Consulta en lote al microservicio de seguridad
            var nombresUsuarios = await _msSeguridadServicio.ObtenerNombresUsuariosPorIds(usuarioIds);
            if (!nombresUsuarios.Correcto)
                throw new KeyNotFoundException("OJO CAMBIAR: NO FUE POSIBLE OBTENER LOS DATOS DEL MICROSERVICIO DE USUARIOS");

            // Crear un diccionario para facilitar la asignación
            var diccionarioUsuarios = nombresUsuarios?.Data?.ToDictionary(u => u.Id, u => u.NombreUsuario);

            // Asignar los nombres a los DTOs
            foreach (var datoConstante in datosConstantesResultado)
            {
                datoConstante.NombreUsuarioCreador = diccionarioUsuarios?.GetValueOrDefault(datoConstante.UsuarioCreadorId);
                if (datoConstante.UsuarioModificadorId is not null)
                    datoConstante.NombreUsuarioModificador = diccionarioUsuarios?.GetValueOrDefault((int)datoConstante.UsuarioModificadorId);
            }

            var datoConstanteDto = _mapper.Map<List<DatoConstanteDto>>(datosConstantesResultado);

            return new ApiResponse<List<DatoConstanteDto>?> { Correcto = true, Mensaje = "", Data = datoConstanteDto };
        }
    }
}    