using Microsoft.AspNetCore.Mvc;
using DCO.Dtos;
using Microsoft.AspNetCore.Authorization;
using DCO.Aplicacion.CasosUso.Interfaces;

namespace ApiDCO.Controllers
{
    [ApiController]
    [Route("api/listas")]
    [Authorize(policy: "ListasPermiso")]
    public class ListaController : Controller
    {
        private readonly IListaServicio _listaServicio;

        public ListaController(IListaServicio listaServicio)
        {
            _listaServicio = listaServicio;
        }

        [HttpGet("obtenerPorId")]
        public async Task<ActionResult<ApiResponse<ListaDto?>>> ObtenerPorId(int id)
        {
            return await _listaServicio.ObtenerPorIdAsync(id);
        }

        [HttpGet("obtenerPorCodigo")]
        public async Task<ActionResult<ApiResponse<ListaDto?>>> ObtenerPorCodigo(string codigo)
        {
            return await _listaServicio.ObtenerPorCodigoAsync(codigo);
        }

        [HttpGet("listar")]
        public async Task<ActionResult<ApiResponse<List<ListaDto>?>>> Listar()
        {
            return await _listaServicio.ListarAsync();
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(ListaCreacionRequest listaCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _listaServicio.CrearAsync(listaCreacionRequest);
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(ListaModificacionRequest listaModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return await _listaServicio.ModificarAsync(listaModificacionRequest);
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id)
        {
            return await _listaServicio.EliminarAsync(id);
        }
    }
}
