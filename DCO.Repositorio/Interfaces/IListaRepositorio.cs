using DCO.Dominio.Entidades;
using DCO.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCO.Repositorio.Interfaces
{
    public interface IListaRepositorio
    {
        Task<int> CrearAsync(DCO_Lista lista);
        Task ModificarAsync(DCO_Lista lista);
        Task<bool> EliminarAsync(int id);
        Task<DCO_Lista?> ObtenerPorIdAsync(int id);
        Task<DCO_Lista?> ObtenerPorCodigoAsync(string codigo);
        IQueryable<ListaDto> Listar();
    }
}
