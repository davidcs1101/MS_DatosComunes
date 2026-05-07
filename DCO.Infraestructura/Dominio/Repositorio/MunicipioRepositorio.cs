using DCO.DataAccess;
using DCO.Dominio.Entidades;
using DCO.Dominio.Entidades.ModelosVistas;
using DCO.Dominio.Repositorio;
using Microsoft.EntityFrameworkCore;

namespace DCO.Infraestructura.Dominio.Repositorio
{
    public class MunicipioRepositorio : IMunicipioRepositorio
    {
        private readonly AppDbContext _context;

        public MunicipioRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UbicacionCompletaMV?> ObtenerUbicacionPorCodigoDepartamentoMunicipio(string codigoDepartamento, string codigoMunicipio)
        {
            return await _context.DCO_Municipios
                .Include(m => m.Departamento)
                    .ThenInclude(d => d.Pais)
                .Where(m => m.Codigo == codigoMunicipio && m.Departamento.Codigo == codigoDepartamento)
                .Select(m => new UbicacionCompletaMV
                {
                    PaisId = m.Departamento.Pais.Id,
                    CodigoPais = m.Departamento.Pais.Codigo,
                    NombrePais = m.Departamento.Pais.Nombre,
                    IndicativoPais = m.Departamento.Pais.Indicativo,
                    EstadoPais = m.Departamento.Pais.EstadoActivo,

                    DepartamentoId = m.Departamento.Id,
                    CodigoDepartamento = m.Departamento.Codigo,
                    NombreDepartamento = m.Departamento.Nombre,
                    IndicativoDepartamento = m.Departamento.Indicativo,
                    EstadoDepartamento = m.Departamento.EstadoActivo,

                    MunicipioId = m.Id,
                    CodigoMunicipio = m.Codigo,
                    NombreMunicipio = m.Nombre,
                    EstadoMunicipio = m.EstadoActivo
                })
                .FirstOrDefaultAsync();
        }
    }
}
