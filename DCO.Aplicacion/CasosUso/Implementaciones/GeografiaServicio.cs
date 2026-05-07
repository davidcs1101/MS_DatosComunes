using AutoMapper;
using DCO.Aplicacion.CasosUso.Interfaces;
using DCO.Aplicacion.Servicios.Interfaces;
using DCO.Dominio.Repositorio;
using DCO.Dtos;

namespace DCO.Aplicacion.CasosUso.Implementaciones
{
    public class GeografiaServicio : IGeografiaServicio
    {
        private readonly IMapper _mapper;
        private readonly IApisResponse _apiResponse;
        private readonly IMunicipioRepositorio _municipioRepositorio;
    
        public GeografiaServicio(IMunicipioRepositorio municipioRepositorio, IMapper mapper, IApisResponse apiResponse)
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
    }
}
