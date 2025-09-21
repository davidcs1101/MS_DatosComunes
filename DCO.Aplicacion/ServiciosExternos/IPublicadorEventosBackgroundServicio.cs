using DCO.Dtos;
using Refit;
using System.ComponentModel.DataAnnotations;

namespace DCO.Aplicacion.ServiciosExternos
{
    public interface IPublicadorEventosBackgroundServicio
    {
        [Post("")]
        Task<HttpResponseMessage> PublicarActualizacionListaDetalle([Url] string urlCompleta, [Body] List<ListaDetalleDto> listaDetalleRequest);
    }
}
