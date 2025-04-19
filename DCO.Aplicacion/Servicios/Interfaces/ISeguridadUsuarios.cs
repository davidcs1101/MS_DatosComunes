using DCO.Aplicacion.ServiciosExternos;
using DCO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Aplicacion.Servicios.Interfaces
{
    public interface ISeguridadUsuarios
    {
        Task<ApiResponse<List<UsuarioDto>?>> Listar(IdsListadoDto idsListadoDto);
    }
}
