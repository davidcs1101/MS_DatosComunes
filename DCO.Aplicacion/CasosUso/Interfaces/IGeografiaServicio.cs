using DCO.Dtos;

namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IGeografiaServicio
    {
        Task<ApiResponse<ListaDto?>> ObtenerUbicacionPorCodigoDepartamentoMunicipio(string codigoDepartamentoMunicipio);
    }
}
