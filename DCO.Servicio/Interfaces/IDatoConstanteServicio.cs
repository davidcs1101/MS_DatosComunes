using DCO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Servicio.Interfaces
{
    public interface IDatoConstanteServicio
    {
        Task<ApiResponse<int>> CrearAsync(DatoConstanteCreacionRequest datoConstanteCreacionRequest);
        Task<ApiResponse<string>> ModificarAsync(DatoConstanteModificacionRequest datoConstanteModificacionRequest);
        Task<ApiResponse<string>> EliminarAsync(int id);
        Task<ApiResponse<DatoConstanteDto?>> ObtenerPorIdAsync(int id);
        Task<ApiResponse<DatoConstanteDto?>> ObtenerPorCodigoAsync(string codigo);
        Task<ApiResponse<List<DatoConstanteDto>?>> ListarAsync();
    }
}
