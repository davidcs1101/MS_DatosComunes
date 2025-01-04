using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DCO.Dominio.Entidades;
using DCO.Dtos;
using DCO.Repositorio.Interfaces;
using DCO.Servicio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Utilidades;

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
        public async Task<ActionResult<ApiResponse<DatoConstanteDto?>>> ObtenerPorId(int id)
        {
            var respuesta = await _datoConstanteServicio.ObtenerPorIdAsync(id);
            return respuesta;
        }

        [HttpGet("obtenerPorCodigo")]
        public async Task<ActionResult<ApiResponse<DatoConstanteDto?>>> ObtenerPorCodigo(string codigo)
        {
            var respuesta = await _datoConstanteServicio.ObtenerPorCodigoAsync(codigo);
            return respuesta;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<ApiResponse<List<DatoConstanteDto>?>>> Listar()
        {
            var datosConstantes = await _datoConstanteServicio.ListarAsync();
            return datosConstantes;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(DatoConstanteCreacionRequest datoConstanteCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = await _datoConstanteServicio.CrearAsync(datoConstanteCreacionRequest);
            return respuesta;
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(DatoConstanteModificacionRequest datoConstanteModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var respuesta = await _datoConstanteServicio.ModificarAsync(datoConstanteModificacionRequest);
            return respuesta;

        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id)
        {
            var respuesta = await _datoConstanteServicio.EliminarAsync(id);
            return respuesta;
        }
    }
}
