using DCO.Dominio.Entidades.ModelosVistas;

namespace DCO.Dominio.Repositorio
{
    public interface IMunicipioRepositorio
    {
        Task<MunicipioMV> ObtenerUbicacionPorMunicipioId(int municipioId);
        Task<MunicipioMV> ObtenerUbicacionPorCodigoDepartamentoMunicipio(string codigoDepartamento, string codigoMunicipio);
        IQueryable<MunicipioMV> ListarUbicacionesPorDepartamentoId(int departamentoId);
    }
}
