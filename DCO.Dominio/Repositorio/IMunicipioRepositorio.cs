using DCO.Dominio.Entidades.ModelosVistas;

namespace DCO.Dominio.Repositorio
{
    public interface IMunicipioRepositorio
    {
        Task<UbicacionCompletaMV?> ObtenerUbicacionPorCodigoDepartamentoMunicipio(string codigoDepartamento, string codigoMunicipio);
        IQueryable<UbicacionCompletaMV> ListarUbicaciones();
    }
}
