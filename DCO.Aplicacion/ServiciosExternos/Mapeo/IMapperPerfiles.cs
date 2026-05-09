using DCO.Dominio.Entidades.ModelosVistas;
using DCO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Aplicacion.ServiciosExternos.Mapeo
{
    public interface IMapperPerfiles
    {
        List<UbicacionCompletaDto> UbicacionesCompletasMVAUbicacionesCompletasDto(List<UbicacionCompletaMV> ubicaciones);
    }
}
