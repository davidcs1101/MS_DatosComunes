using AutoMapper;
using DCO.Dtos;
using DCO.Dominio.Entidades;
using DCO.Dominio.Entidades.ModelosVistas;

namespace DCO.Infraestructura.Mapeos
{
    public class AutoMapperPerfiles : Profile
    {
        public AutoMapperPerfiles() 
        {
            CreateMap<ListaCreacionRequest, DCO_Lista>();
            CreateMap<ListaModificacionRequest, DCO_Lista>();
            CreateMap<DCO_Lista, ListaDto>();

            CreateMap<ListaDetalleCreacionRequest, DCO_ListaDetalle>();
            CreateMap<ListaDetalleModificacionRequest, DCO_ListaDetalle>();

            CreateMap<DatoConstanteCreacionRequest, DCO_DatoConstante>();
            CreateMap<DatoConstanteModificacionRequest, DCO_DatoConstante>();
            CreateMap<DCO_DatoConstante, DatoConstanteDto>();

            CreateMap<ListaDetalleMV, ListaDetalleDto>();


        }
    }
}
