using Microsoft.AspNetCore.Mvc;
using DCO.Dtos;
using Microsoft.AspNetCore.Authorization;
using DCO.Aplicacion.CasosUso.Interfaces;
using Utilidades.Dtos;

namespace ApiDCO.Controllers
{
    [ApiController]
    [Route("api/datosConstantes")]
    [Authorize(policy: "DatosConstantesPermiso")]
    public class DatoConstanteController : Controller
    {
        private readonly IDatoConstanteServicio _datoConstanteServicio;

        public DatoConstanteController(IDatoConstanteServicio datoConstanteServicio)
        {
            _datoConstanteServicio = datoConstanteServicio;
        }

        [HttpGet("obtenerPorId")]
        public async Task<ActionResult<ApiResponseDto<DatoConstanteDto?>>> ObtenerPorId(int id)
        {
            return await _datoConstanteServicio.ObtenerPorIdAsync(id);
        }

        [HttpGet("obtenerPorCodigo")]
        public async Task<ActionResult<ApiResponseDto<DatoConstanteDto?>>> ObtenerPorCodigo(string codigo)
        {
            return await _datoConstanteServicio.ObtenerPorCodigoAsync(codigo);
        }

        [HttpGet("listar")]
        public async Task<ActionResult<ApiResponseDto<List<DatoConstanteDto>?>>> Listar()
        {
            return await _datoConstanteServicio.ListarAsync();
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponseDto<int>>> Crear(DatoConstanteCreacionRequest datoConstanteCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _datoConstanteServicio.CrearAsync(datoConstanteCreacionRequest);
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponseDto<string>>> Modificar(DatoConstanteModificacionRequest datoConstanteModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _datoConstanteServicio.ModificarAsync(datoConstanteModificacionRequest);
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<ApiResponseDto<string>>> Eliminar(int id)
        {
            return await _datoConstanteServicio.EliminarAsync(id);
        }
    }
}
