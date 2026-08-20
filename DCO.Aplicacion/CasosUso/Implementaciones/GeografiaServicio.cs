using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Aplicacion.ServiciosExternos.Mapeo;
using DCO.Dominio.Repositorio;
using DCO.Dtos;
using Microsoft.EntityFrameworkCore;
using Utilidades.Dtos;
using Utilidades.Servicios.Responses.Interfaces;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class GeografiaServicio : IGeografiaServicio
    {
        private readonly IApiResponse _apiResponse;
        private readonly IMunicipioRepositorio _municipioRepositorio;
        private readonly IMapperPerfiles _mapper;

        public GeografiaServicio(IMunicipioRepositorio municipioRepositorio, IMapperPerfiles mapper, IApiResponse apiResponse)
        {
            _municipioRepositorio = municipioRepositorio;
            _mapper = mapper;
            _apiResponse = apiResponse;
        }

        public Task<ApiResponseDto<ListaDto?>> ObtenerUbicacionPorCodigoDepartamentoMunicipio(string codigoDepartamentoMunicipio)
        {

            if (codigoDepartamentoMunicipio.Length!=5)
            {

            }

            var Departamento = codigoDepartamentoMunicipio[..2];
            var Municipio = codigoDepartamentoMunicipio.Substring(2, 3);
            throw new NotImplementedException();
        }

        public async Task<ApiResponseDto<List<UbicacionCompletaDto>?>> ListarAsync()
        {
            var ubicaciones = await _municipioRepositorio.ListarUbicaciones().ToListAsync();
            var ubicacionesResultado = _mapper.UbicacionesCompletasMVAUbicacionesCompletasDto(ubicaciones);
            return _apiResponse.CrearRespuesta<List<UbicacionCompletaDto>?>(true, "", ubicacionesResultado);
        }
    }
}
