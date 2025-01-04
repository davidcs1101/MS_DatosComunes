using DCO.Dominio.Entidades;
using DCO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Repositorio.Interfaces
{
    public interface IDatoConstanteRepositorio
    {
        Task<int> CrearAsync(DCO_DatoConstante DatoConstante);
        Task ModificarAsync(DCO_DatoConstante DatoConstante);
        Task<bool> EliminarAsync(int id);
        Task<DCO_DatoConstante?> ObtenerPorIdAsync(int id);
        Task<DCO_DatoConstante?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<DatoConstanteDto> Listar();
    }
}
