using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCO.DataAccess;
using DCO.Dominio.Entidades;
using DCO.Dtos;
using DCO.Repositorio.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DCO.Repositorio.Implementaciones
{
    public class DatoConstanteRepositorio : IDatoConstanteRepositorio
    {
        private readonly AppDbContext _context;
        public DatoConstanteRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> CrearAsync(DCO_DatoConstante datoConstante)
        {
            _context.DCO_DatosConstantes.Add(datoConstante);
            await _context.SaveChangesAsync();
            return datoConstante.Id;
        }

        public async Task ModificarAsync(DCO_DatoConstante datoConstante)
        {
            _context.DCO_DatosConstantes.Update(datoConstante);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var eliminado = await _context.DCO_DatosConstantes.Where(u => u.Id == id).ExecuteDeleteAsync();
            return eliminado > 0;
        }

        public async Task<DCO_DatoConstante?> ObtenerPorIdAsync(int id)
        {
            return await _context.DCO_DatosConstantes.FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<DCO_DatoConstante?> ObtenerPorCodigoAsync(string codigo)
        {
            return await _context.DCO_DatosConstantes.FirstOrDefaultAsync(g => g.Codigo == codigo);
        }

        public IQueryable<DatoConstanteDto> Listar()
        {
            var datoConstantes = _context.DCO_DatosConstantes
                .Select(g => new DatoConstanteDto
                {
                    Id = g.Id,
                    Codigo = g.Codigo,
                    Nombre = g.Nombre,
                    UsuarioCreadorId = g.UsuarioCreadorId,
                    FechaCreado = g.FechaCreado,
                    UsuarioModificadorId = g.UsuarioModificadorId,
                    FechaModificado = g.FechaModificado,
                    EstadoActivo = g.EstadoActivo
                });
            return datoConstantes;
        }
    }
}
