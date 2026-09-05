using Microsoft.AspNetCore.Mvc;
using DCO.Dtos;
using Microsoft.AspNetCore.Authorization;
using DCO.Aplicacion.CasosUso.Interfaces;
using Utilidades.Seguridad;
using Utilidades.Dtos;
using DCO.Api.DatosComunes.Middlewares.Permisos;

namespace ApiDCO.Controllers
{
    [ApiController]
    [Route("api/datosConstantes")]
    [Authorize]
    public class DatoConstanteController : Controller
    {
        private readonly IDatoConstanteServicio _datoConstanteServicio;

        public DatoConstanteController(IDatoConstanteServicio datoConstanteServicio)
        {
            _datoConstanteServicio = datoConstanteServicio;
        }

        [HttpGet("obtenerPorId")]
        [Permiso(CodigosPermisos.DatosConstantes.CONSULTAR)]
        public async Task<ActionResult<ApiResponseDto<DatoConstanteDto?>>> ObtenerPorId(int id)
        {
            return await _datoConstanteServicio.ObtenerPorIdAsync(id);
        }

        [HttpGet("obtenerPorCodigo")]
        [Permiso(CodigosPermisos.DatosConstantes.CONSULTAR)]
        public async Task<ActionResult<ApiResponseDto<DatoConstanteDto?>>> ObtenerPorCodigo(string codigo)
        {
            return await _datoConstanteServicio.ObtenerPorCodigoAsync(codigo);
        }

        [HttpGet("listar")]
        [Permiso(CodigosPermisos.DatosConstantes.LISTAR)]
        public async Task<ActionResult<ApiResponseDto<List<DatoConstanteDto>?>>> Listar()
        {
            return await _datoConstanteServicio.ListarAsync();
        }

        [HttpPost("crear")]
        [Permiso(CodigosPermisos.DatosConstantes.CREAR)]
        public async Task<ActionResult<ApiResponseDto<int>>> Crear(DatoConstanteCreacionRequest datoConstanteCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _datoConstanteServicio.CrearAsync(datoConstanteCreacionRequest);
        }

        [HttpPut("modificar")]
        [Permiso(CodigosPermisos.DatosConstantes.MODIFICAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Modificar(DatoConstanteModificacionRequest datoConstanteModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _datoConstanteServicio.ModificarAsync(datoConstanteModificacionRequest);
        }

        [HttpDelete("eliminar")]
        [Permiso(CodigosPermisos.DatosConstantes.ELIMINAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Eliminar(int id)
        {
            return await _datoConstanteServicio.EliminarAsync(id);
        }
    }
}
