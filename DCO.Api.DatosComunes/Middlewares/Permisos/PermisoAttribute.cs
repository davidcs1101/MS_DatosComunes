using Microsoft.AspNetCore.Authorization;
namespace DCO.Api.DatosComunes.Middlewares.Permisos
{
    public class PermisoAttribute : AuthorizeAttribute
    {
        public string Permiso { get; }

        public PermisoAttribute(string permiso)
        {
            Permiso = permiso;
            Policy = "Permiso";
        }
    }
}
