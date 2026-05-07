using DCO.Dominio.Entidades.ModelosVistas;

namespace DCO.Dominio.Repositorio
{
    public interface IMunicipioRepositorio
    {
        Task<UbicacionCompletaMV?> ObtenerUbicacionPorCodigoDepartamentoMunicipio(string codigoDepartamento, string codigoMunicipio);
        //Task<UbicacionCompletaMV> ObtenerUbicacionPorMunicipioId(int municipioId);
        //IQueryable<UbicacionCompletaMV> ListarUbicacionesPorDepartamentoId(int departamentoId);
    }
}
