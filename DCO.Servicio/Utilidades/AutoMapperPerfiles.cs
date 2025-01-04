using AutoMapper;
using DCO.Dtos;
using DCO.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Servicio.Utilidades
{
    public class AutoMapperPerfiles : Profile
    {
        public AutoMapperPerfiles() 
        {
            CreateMap<ListaCreacionRequest, DCO_Lista>();
            CreateMap<ListaModificacionRequest, DCO_Lista>();
            CreateMap<DCO_Lista, ListaDto>();

            CreateMap<DatoConstanteCreacionRequest, DCO_DatoConstante>();
            CreateMap<DatoConstanteModificacionRequest, DCO_DatoConstante>();
            CreateMap<DCO_DatoConstante, DatoConstanteDto>();
        }
    }
}
