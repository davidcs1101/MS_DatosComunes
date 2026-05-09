using AutoMapper;
using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Aplicacion.ServiciosExternos.Mapeo;
using DCO.Dominio.Repositorio;
using DCO.Dtos;
using Microsoft.EntityFrameworkCore;
using Utilidades;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class GeografiaServicio : IGeografiaServicio
    {
        private readonly IApisResponse _apiResponse;
        private readonly IMunicipioRepositorio _municipioRepositorio;
        private readonly IMapperPerfiles _mapper;

        public GeografiaServicio(IMunicipioRepositorio municipioRepositorio, IMapperPerfiles mapper, IApisResponse apiResponse)
        {
            _municipioRepositorio = municipioRepositorio;
            _mapper = mapper;
            _apiResponse = apiResponse;
        }

        public Task<ApiResponse<ListaDto?>> ObtenerUbicacionPorCodigoDepartamentoMunicipio(string codigoDepartamentoMunicipio)
        {

            if (codigoDepartamentoMunicipio.Length!=5)
            {

            }

            var Departamento = codigoDepartamentoMunicipio[..2];
            var Municipio = codigoDepartamentoMunicipio.Substring(2, 3);
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<List<UbicacionCompletaDto>?>> ListarAsync()
        {
            var ubicaciones = await _municipioRepositorio.ListarUbicaciones().ToListAsync();
            var ubicacionesResultado = _mapper.UbicacionesCompletasMVAUbicacionesCompletasDto(ubicaciones);
            return _apiResponse.CrearRespuesta<List<UbicacionCompletaDto>?>(true, "", ubicacionesResultado);
        }
    }
}
