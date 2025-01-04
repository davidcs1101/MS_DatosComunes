using DCO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Servicio.Interfaces
{
    public interface IListaDetalleServicio
    {
        Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoListaAsync(string codigoLista);
        Task<ApiResponse<List<ListaDetalleDto>?>> ListarPorCodigoConstanteAsync(string codigoConstante);
    }
}
