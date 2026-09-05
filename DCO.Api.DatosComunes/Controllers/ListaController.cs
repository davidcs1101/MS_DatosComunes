using DCO.Api.DatosComunes.Middlewares.Permisos;
using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilidades.Dtos;
using Utilidades.Seguridad;

namespace ApiDCO.Controllers
{
    [ApiController]
    [Route("api/listas")]
    [Authorize]
    public class ListaController : Controller
    {
        private readonly IListaServicio _listaServicio;

        public ListaController(IListaServicio listaServicio)
        {
            _listaServicio = listaServicio;
        }

        [HttpGet("obtenerPorId")]
        [Permiso(CodigosPermisos.Listas.CONSULTAR)]
        public async Task<ActionResult<ApiResponseDto<ListaDto?>>> ObtenerPorId(int id)
        {
            return await _listaServicio.ObtenerPorIdAsync(id);
        }

        [HttpGet("obtenerPorCodigo")]
        [Permiso(CodigosPermisos.Listas.CONSULTAR)]
        public async Task<ActionResult<ApiResponseDto<ListaDto?>>> ObtenerPorCodigo(string codigo)
        {
            return await _listaServicio.ObtenerPorCodigoAsync(codigo);
        }

        [HttpGet("listar")]
        [Permiso(CodigosPermisos.Listas.LISTAR)]
        public async Task<ActionResult<ApiResponseDto<List<ListaDto>?>>> Listar()
        {
            return await _listaServicio.ListarAsync();
        }

        [HttpPost("crear")]
        [Permiso(CodigosPermisos.Listas.CREAR)]
        public async Task<ActionResult<ApiResponseDto<int>>> Crear(ListaCreacionRequest listaCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _listaServicio.CrearAsync(listaCreacionRequest);
        }

        [HttpPut("modificar")]
        [Permiso(CodigosPermisos.Listas.MODIFICAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Modificar(ListaModificacionRequest listaModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _listaServicio.ModificarAsync(listaModificacionRequest);
        }

        [HttpDelete("eliminar")]
        [Permiso(CodigosPermisos.Listas.ELIMINAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Eliminar(int id)
        {
            return await _listaServicio.EliminarAsync(id);
        }
    }
}
