using DCO.Dominio.Entidades;
using DCO.Dominio.Excepciones;
using DCO.Dominio.Servicios.Interfaces;

namespace DCO.Dominio.Servicios.Implementaciones
{
    public class DatoConstanteValidador : IDatoConstanteValidador
    {
        public void ValidarDatoYaExiste(DCO_DatoConstante? datoConstante, string mensaje) {
            if (datoConstante != null)
                throw new DatoYaExisteException(mensaje);
        }

        public void ValidarDatoNoEncontrado(DCO_DatoConstante? datoConstante, string mensaje)
        {
            if (datoConstante == null)
                throw new DatoNoEncontradoException(mensaje);
        }
    }
}
