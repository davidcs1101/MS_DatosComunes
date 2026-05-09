using DCO.Aplicacion.ServiciosExternos.Mapeo;
using DCO.Dominio.Entidades.ModelosVistas;
using DCO.Dtos;
using Riok.Mapperly.Abstractions;

namespace DCO.Infraestructura.Mapeo
{
    [Mapper]
    public partial class MapperPerfiles : IMapperPerfiles
    {
        public partial List<UbicacionCompletaDto> UbicacionesCompletasMVAUbicacionesCompletasDto(
            List<UbicacionCompletaMV> ubicaciones);
    }
}
