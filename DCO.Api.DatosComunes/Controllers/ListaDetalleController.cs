using Microsoft.AspNetCore.Mvc;
using DCO.Dtos;
using DCO.Aplicacion.CasosUso.Interfaces;
using Utilidades.Dtos;
using Microsoft.AspNetCore.Authorization;
using DCO.Api.DatosComunes.Middlewares.Permisos;
using Utilidades.Seguridad;

namespace ApiDCO.Controllers
{
    [ApiController]
    [Route("api/listasDetalles")]
    [Authorize]
    public class ListaDetalleController : Controller
    {
        private readonly IListaDetalleServicio _listaDetalleServicio;

        public ListaDetalleController(IListaDetalleServicio listaDetalleServicio)
        {
            _listaDetalleServicio = listaDetalleServicio;
        }

        [HttpPost("crear")]
        [Permiso(CodigosPermisos.ListasDetalles.CREAR)]
        public async Task<ActionResult<ApiResponseDto<int>>> Crear(ListaDetalleCreacionRequest listaDetalleCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _listaDetalleServicio.CrearAsync(listaDetalleCreacionRequest);
        }

        [HttpPut("modificar")]
        [Permiso(CodigosPermisos.ListasDetalles.MODIFICAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Modificar(ListaDetalleModificacionRequest listaDetalleModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _listaDetalleServicio.ModificarAsync(listaDetalleModificacionRequest);
        }

        [HttpDelete("eliminar")]
        [Permiso(CodigosPermisos.ListasDetalles.ELIMINAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Eliminar(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _listaDetalleServicio.EliminarAsync(id);
        }

        [HttpGet("listarPorCodigoLista")]
        [Permiso(CodigosPermisos.ListasDetalles.LISTAR)]
        public async Task<ActionResult<ApiResponseDto<List<ListaDetalleDto>?>>> ListarPorcodigoLista(string codigoLista)
        {
            return await _listaDetalleServicio.ListarPorCodigoListaAsync(codigoLista);
        }

        [HttpGet("listarPorCodigoConstante")]
        [Permiso(CodigosPermisos.ListasDetalles.LISTAR)]
        public async Task<ActionResult<ApiResponseDto<List<ListaDetalleDto>?>>> ListarPorcodigoConstante(string codigoConstante)
        {
            return await _listaDetalleServicio.ListarPorCodigoConstanteAsync(codigoConstante);
        }

        [HttpPost("obtenerPorCodigoConstanteYCodigoListaDetalle")]
        [Permiso(CodigosPermisos.ListasDetalles.CONSULTAR)]
        public async Task<ActionResult<ApiResponseDto<ListaDetalleDto?>>> ObtenerPorCodigoConstanteYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest)
        {
            return await _listaDetalleServicio.ObtenerPorCodigoConstanteYCodigoListaDetalle(codigoDetalleRequest);
        }

        [HttpPost("obtenerPorCodigoListaYCodigoListaDetalle")]
        [Permiso(CodigosPermisos.ListasDetalles.CONSULTAR)]
        public async Task<ActionResult<ApiResponseDto<ListaDetalleDto?>>> ObtenerPorCodigoListaYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest)
        {
            return await _listaDetalleServicio.ObtenerPorCodigoListaYCodigoListaDetalle(codigoDetalleRequest);
        }

        [HttpPost("listarPorCodigosLista")]
        [Permiso(CodigosPermisos.ListasDetalles.LISTAR)]
        public async Task<ActionResult<ApiResponseDto<List<ListaDetalleDto>?>>> ListarPorCodigosLista(List<string> codigosLista)
        {
            return await _listaDetalleServicio.ListarPorCodigosListaAsync(codigosLista);
        }

        [HttpPost("listarPorCodigosConstante")]
        [Permiso(CodigosPermisos.ListasDetalles.LISTAR)]
        public async Task<ActionResult<ApiResponseDto<List<ListaDetalleDto>?>>> ListarPorCodigosConstante(List<string> codigosConstante)
        {
            return await _listaDetalleServicio.ListarPorCodigosConstanteAsync(codigosConstante);
        }

    }
}
