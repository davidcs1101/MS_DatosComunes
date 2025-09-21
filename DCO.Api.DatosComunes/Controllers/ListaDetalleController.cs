using Microsoft.AspNetCore.Mvc;
using DCO.Dtos;
using DCO.Aplicacion.CasosUso.Interfaces;

namespace ApiDCO.Controllers
{
    [ApiController]
    [Route("api/listasDetalles")]
    public class ListaDetalleController : Controller
    {
        private readonly IListaDetalleServicio _listaDetalleServicio;

        public ListaDetalleController(IListaDetalleServicio listaDetalleServicio)
        {
            _listaDetalleServicio = listaDetalleServicio;
        }

        [HttpPost("crear")]
        public async Task<ActionResult<ApiResponse<int>>> Crear(ListaDetalleCreacionRequest listaDetalleCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _listaDetalleServicio.CrearAsync(listaDetalleCreacionRequest);
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponse<string>>> Modificar(ListaDetalleModificacionRequest listaDetalleModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _listaDetalleServicio.ModificarAsync(listaDetalleModificacionRequest);
        }

        [HttpGet("listarPorCodigoLista")]
        public async Task<ActionResult<ApiResponse<List<ListaDetalleDto>?>>> ListarPorcodigoLista(string codigoLista)
        {
            var listasDetatalles = await _listaDetalleServicio.ListarPorCodigoListaAsync(codigoLista);
            return listasDetatalles;
        }

        [HttpGet("listarPorCodigoConstante")]
        public async Task<ActionResult<ApiResponse<List<ListaDetalleDto>?>>> ListarPorcodigoConstante(string codigoConstante)
        {
            var listasDetatalles = await _listaDetalleServicio.ListarPorCodigoConstanteAsync(codigoConstante);
            return listasDetatalles;
        }

        [HttpPost("obtenerPorCodigoConstanteYCodigoListaDetalle")]
        public async Task<ActionResult<ApiResponse<ListaDetalleDto?>>> ObtenerPorCodigoConstanteYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest)
        {
            var listasDetatalles = await _listaDetalleServicio.ObtenerPorCodigoConstanteYCodigoListaDetalle(codigoDetalleRequest);
            return listasDetatalles;
        }

        [HttpPost("obtenerPorCodigoListaYCodigoListaDetalle")]
        public async Task<ActionResult<ApiResponse<ListaDetalleDto?>>> ObtenerPorCodigoListaYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest)
        {
            var listasDetatalles = await _listaDetalleServicio.ObtenerPorCodigoListaYCodigoListaDetalle(codigoDetalleRequest);
            return listasDetatalles;
        }
    }
}
