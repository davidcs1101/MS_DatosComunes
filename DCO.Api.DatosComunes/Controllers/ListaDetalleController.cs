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

        [HttpPost("validarIdDetalleExisteEnCodigoLista")]
        public async Task<ActionResult<ApiResponse<string>>> ValidarIdDetalleExisteEnCodigoLista(CodigoIdListaDetalleRequest codigoListaIdDetalleRequest)
        {
            var listasDetatalles = await _listaDetalleServicio.ValidarIdDetalleExisteEnCodigoListaAsync(codigoListaIdDetalleRequest);
            return listasDetatalles;
        }

        [HttpPost("obtenerPorCodigoConstanteYCodigoListaDetalle")]
        public async Task<ActionResult<ApiResponse<ListaDetalleDto?>>> ObtenerPorCodigoConstanteYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest)
        {
            var listasDetatalles = await _listaDetalleServicio.ObtenerPorCodigoConstanteYCodigoListaDetalle(codigoDetalleRequest);
            return listasDetatalles;
        }
    }
}
