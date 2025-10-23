using Microsoft.AspNetCore.Mvc;
using DCO.Dtos;
using Microsoft.AspNetCore.Authorization;
using DCO.Aplicacion.CasosUso.Interfaces;

namespace ApiDCO.Controllers
{
    [ApiController]
    [Route("api/datosConstantesDetalles")]
    [Authorize(policy: "DatosConstantesPermiso")]
    public class DatoConstanteDetalleController : Controller
    {
        private readonly IDatoConstanteDetalleServicio _datoConstanteDetalleServicio;

        public DatoConstanteDetalleController(IDatoConstanteDetalleServicio datoConstanteDetalleServicio)
        {
            _datoConstanteDetalleServicio = datoConstanteDetalleServicio;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(DatoConstanteDetalleCreacionRequest datoConstanteDetalleCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _datoConstanteDetalleServicio.CrearAsync(datoConstanteDetalleCreacionRequest);
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(DatoConstanteDetalleModificacionRequest datoConstanteDetalleModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _datoConstanteDetalleServicio.ModificarAsync(datoConstanteDetalleModificacionRequest);
        }
    }
}
