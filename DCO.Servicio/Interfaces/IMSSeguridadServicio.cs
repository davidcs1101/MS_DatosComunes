using DCO.Dtos;
using DCO.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Servicio.Interfaces
{
    public interface IMSSeguridadServicio
    {
        Task<ApiResponse<string>> ObtenerNombreUsuarioPorIdAsync(int id);
        Task<ApiResponse<List<UsuarioDto>?>> ObtenerNombresUsuariosPorIds(List<int?>? usuarioIds);
    }
}
