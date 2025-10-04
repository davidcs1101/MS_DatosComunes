using DCO.Dtos;

namespace DCO.Aplicacion.ServiciosExternos
{
    public interface IPublicadorEventosBackgroundServicio
    {
        Task<HttpResponseMessage> PublicarActualizacionListaDetalle(string url,List<ListaDetalleDto> listaDetalleRequest);
    }
}
