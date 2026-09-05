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
    [Route("api/datosConstantesDetalles")]
    [Authorize]
    public class DatoConstanteDetalleController : Controller
    {
        private readonly IDatoConstanteDetalleServicio _datoConstanteDetalleServicio;

        public DatoConstanteDetalleController(IDatoConstanteDetalleServicio datoConstanteDetalleServicio)
        {
            _datoConstanteDetalleServicio = datoConstanteDetalleServicio;
        }

        [HttpPost("crear")]
        [Permiso(CodigosPermisos.DatosConstantesDetalles.CREAR)]
        public async Task<ActionResult<ApiResponseDto<int>>> Crear(DatoConstanteDetalleCreacionRequest datoConstanteDetalleCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _datoConstanteDetalleServicio.CrearAsync(datoConstanteDetalleCreacionRequest);
        }

        [HttpPut("modificar")]
        [Permiso(CodigosPermisos.DatosConstantesDetalles.MODIFICAR)]
        public async Task<ActionResult<ApiResponseDto<string>>> Modificar(DatoConstanteDetalleModificacionRequest datoConstanteDetalleModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _datoConstanteDetalleServicio.ModificarAsync(datoConstanteDetalleModificacionRequest);
        }
    }
}
