using Microsoft.AspNetCore.Mvc;
using DCO.Dtos;
using DCO.Aplicacion.CasosUso.Interfaces;
using Utilidades.Dtos;

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
        public async Task<ActionResult<ApiResponseDto<int>>> Crear(ListaDetalleCreacionRequest listaDetalleCreacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _listaDetalleServicio.CrearAsync(listaDetalleCreacionRequest);
        }

        [HttpPut("modificar")]
        public async Task<ActionResult<ApiResponseDto<string>>> Modificar(ListaDetalleModificacionRequest listaDetalleModificacionRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _listaDetalleServicio.ModificarAsync(listaDetalleModificacionRequest);
        }

        [HttpDelete("eliminar")]
        public async Task<ActionResult<ApiResponseDto<string>>> Eliminar(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _listaDetalleServicio.EliminarAsync(id);
        }

        [HttpGet("listarPorCodigoLista")]
        public async Task<ActionResult<ApiResponseDto<List<ListaDetalleDto>?>>> ListarPorcodigoLista(string codigoLista)
        {
            return await _listaDetalleServicio.ListarPorCodigoListaAsync(codigoLista);
        }

        [HttpGet("listarPorCodigoConstante")]
        public async Task<ActionResult<ApiResponseDto<List<ListaDetalleDto>?>>> ListarPorcodigoConstante(string codigoConstante)
        {
            return await _listaDetalleServicio.ListarPorCodigoConstanteAsync(codigoConstante);
        }

        [HttpPost("obtenerPorCodigoConstanteYCodigoListaDetalle")]
        public async Task<ActionResult<ApiResponseDto<ListaDetalleDto?>>> ObtenerPorCodigoConstanteYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest)
        {
            return await _listaDetalleServicio.ObtenerPorCodigoConstanteYCodigoListaDetalle(codigoDetalleRequest);
        }

        [HttpPost("obtenerPorCodigoListaYCodigoListaDetalle")]
        public async Task<ActionResult<ApiResponseDto<ListaDetalleDto?>>> ObtenerPorCodigoListaYCodigoListaDetalle(CodigoDetalleRequest codigoDetalleRequest)
        {
            return await _listaDetalleServicio.ObtenerPorCodigoListaYCodigoListaDetalle(codigoDetalleRequest);
        }

        [HttpPost("listarPorCodigosLista")]
        public async Task<ActionResult<ApiResponseDto<List<ListaDetalleDto>?>>> ListarPorCodigosLista(List<string> codigosLista)
        {
            return await _listaDetalleServicio.ListarPorCodigosListaAsync(codigosLista);
        }

        [HttpPost("listarPorCodigosConstante")]
        public async Task<ActionResult<ApiResponseDto<List<ListaDetalleDto>?>>> ListarPorCodigosConstante(List<string> codigosConstante)
        {
            return await _listaDetalleServicio.ListarPorCodigosConstanteAsync(codigosConstante);
        }

    }
}
