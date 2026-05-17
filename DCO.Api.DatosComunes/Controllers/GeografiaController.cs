using DCO.Aplicacion.CasosUso.Implementaciones;
using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DCO.Api.DatosComunes.Controllers
{
    [ApiController]
    [Route("api/geografia")]
    //[Authorize(policy: "DatosConstantesPermiso")]
    public class GeografiaController : Controller
    {
        private readonly IGeografiaServicio _geografiaServicio;

        public GeografiaController(IGeografiaServicio geografiaServicio)
        {
            _geografiaServicio = geografiaServicio;
        }

        [HttpGet("listar")]
        public async Task<ApiResponse<List<UbicacionCompletaDto>?>> Listar()
        {
            return await _geografiaServicio.ListarAsync();
        }
    }
}
