using DCO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Servicio.Interfaces
{
    public interface IListaServicio
    {
        Task<ApiResponse<int>> CrearAsync(ListaCreacionRequest listaCreacionRequest);
        Task<ApiResponse<string>> ModificarAsync(ListaModificacionRequest listaModificacionRequest);
        Task<ApiResponse<string>> EliminarAsync(int id);
        Task<ApiResponse<ListaDto?>> ObtenerPorIdAsync(int id);
        Task<ApiResponse<ListaDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponse<List<ListaDto>?>> ListarAsync();
    }
}
