using DCO.Api.DatosComunes.Middlewares.Permisos;
using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilidades.Dtos;
using Utilidades.Seguridad;

namespace DCO.Api.DatosComunes.Controllers
{
    [ApiController]
    [Route("api/geografia")]
    [Authorize]
    public class GeografiaController : Controller
    {
        private readonly IGeografiaServicio _geografiaServicio;

        public GeografiaController(IGeografiaServicio geografiaServicio)
        {
            _geografiaServicio = geografiaServicio;
        }

        [HttpGet("listar")]
        [Permiso(CodigosPermisos.Geografia.LISTAR)]
        public async Task<ApiResponseDto<List<UbicacionCompletaDto>?>> Listar()
        {
            return await _geografiaServicio.ListarAsync();
        }
    }
}
