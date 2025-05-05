using DCO.Dominio.Entidades;
using DCO.Dominio.Excepciones;
using DCO.Dominio.Servicios.Interfaces;

namespace DCO.Dominio.Servicios.Implementaciones
{
    public class ListaValidador : IListaValidador
    {
        public void ValidarDatoYaExiste(DCO_Lista? lista, string mensaje) {
            if (lista != null)
                throw new DatoYaExisteException(mensaje);
        }

        public void ValidarDatoNoEncontrado(DCO_Lista? lista, string mensaje)
        {
            if (lista == null)
                throw new DatoNoEncontradoException(mensaje);
        }
    }
}
