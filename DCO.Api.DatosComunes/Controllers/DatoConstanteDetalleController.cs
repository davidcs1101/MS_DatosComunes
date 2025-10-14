using Microsoft.AspNetCore.Mvc;
using DCO.Dtos;
using Microsoft.AspNetCore.Authorization;
using DCO.Aplicacion.CasosUso.Interfaces;

namespace ApiDCO.Controllers
{
    [ApiController]
    [Route("api/datosConstantesDetalles")]
    //[Authorize(policy: "DatosConstantesPermiso")]
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

            var respuesta = await _datoConstanteDetalleServicio.CrearAsync(datoConstanteDetalleCreacionRequest);
            return respuesta;
        }

        //[HttpPut("modificar")]
        //public async Task<ActionResult<ApiResponse<string>>> Modificar(DatoConstanteModificacionRequest datoConstanteModificacionRequest)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var respuesta = await _datoConstanteServicio.ModificarAsync(datoConstanteModificacionRequest);
        //    return respuesta;

        //}

        //[HttpDelete("eliminar")]
        //public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id)
        //{
        //    var respuesta = await _datoConstanteServicio.EliminarAsync(id);
        //    return respuesta;
        //}
    }
}
