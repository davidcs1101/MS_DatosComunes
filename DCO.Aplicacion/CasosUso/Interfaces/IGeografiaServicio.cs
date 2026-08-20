using DCO.Dtos;
using Utilidades.Dtos;
namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IGeografiaServicio
    {
        Task<ApiResponseDto<ListaDto?>> ObtenerUbicacionPorCodigoDepartamentoMunicipio(string codigoDepartamentoMunicipio);
        Task<ApiResponseDto<List<UbicacionCompletaDto>?>> ListarAsync();
    }
}
