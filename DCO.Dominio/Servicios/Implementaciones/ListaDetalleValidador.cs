using DCO.Dominio.Entidades;
using DCO.Dominio.Excepciones;
using DCO.Dominio.Servicios.Interfaces;

namespace DCO.Dominio.Servicios.Implementaciones
{
    public class ListaDetalleValidador : IListaDetalleValidador
    {
        public void ValidarDatoYaExiste(DCO_ListaDetalle? listaDetalle, string mensaje) {
            if (listaDetalle != null)
                throw new DatoYaExisteException(mensaje);
        }

        public void ValidarDatoNoEncontrado(DCO_ListaDetalle? listaDetalle, string mensaje)
        {
            if (listaDetalle == null)
                throw new DatoNoEncontradoException(mensaje);
        }
    }
}
