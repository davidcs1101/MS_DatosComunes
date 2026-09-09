using DCO.Aplicacion.CasosUso.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilidades.Dtos;
using Utilidades.Seguridad;

namespace ApiDCO.Controllers
{
    [ApiController]
    [Route("api/recepcionEventos")]
    [Authorize(Policy = Politicas.GRUPOSFUNCIONESSISTEMA)]
    public class RecepcionEventoController : Controller
    {
        private readonly IColaSolicitudServicio _colaSolicitudServicio;
        public RecepcionEventoController(IColaSolicitudServicio colaSolicitudServicio)
        {
            _colaSolicitudServicio = colaSolicitudServicio; 
        }

        [HttpPost("recibirEvento")]
        public async Task<ActionResult<ApiResponseDto<int>>> RecibirEvento(ColaSolicitudCreacionRequest colaSolicitudCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _colaSolicitudServicio.CrearAsync(colaSolicitudCreacionRequest);
        }
    }
}
