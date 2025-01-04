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
            var respuesta = await _listaServicio.ObtenerPorIdAsync(id);
            return respuesta;
        }

        [HttpGet("obtenerPorCodigo")]
        public async Task<ActionResult<ApiResponse<ListaDto?>>> ObtenerPorCodigo(string codigo)
        {
            var respuesta = await _listaServicio.ObtenerPorCodigoAsync(codigo);
            return respuesta;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<ApiResponse<List<ListaDto>?>>> Listar()
        {
            var listas = await _listaServicio.ListarAsync();
            return listas;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(ListaCreacionRequest listaCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var respuesta = await _listaServicio.CrearAsync(listaCreacionRequest);
            return respuesta;
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(ListaModificacionRequest listaModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var respuesta = await _listaServicio.ModificarAsync(listaModificacionRequest);
            return respuesta;
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<ApiResponse<string>>> Eliminar(int id)
        {
            var respuesta = await _listaServicio.EliminarAsync(id);
            return respuesta;
        }
    }
}
