using DCO.Dtos;
using Refit;

namespace DCO.Aplicacion.ServiciosExternos
{
    public interface IMSSeguridadBackgroundServicio
    {
        [Post("/autenticacion/autenticarUsuario")]
        Task<HttpResponseMessage> AutenticarUsuarioAsync([Body] AutenticacionRequest autenticacionRequest);
    }
}
