using DCO.Dominio.Entidades;
using Utilidades.Dtos;

namespace DCO.Aplicacion.CasosUso.Interfaces
{
    public interface IColaSolicitudServicio
    {
        Task ProcesarColaSolicitudesAsync();
        Task ProcesarPorColaSolicitudIdAsync(int id, bool validarEstadoPendiente = false);
        Task<ApiResponseDto<int>> CrearAsync(ColaSolicitudCreacionRequest colaSolicitudCreacionRequest);
        Task<DCO_ColaSolicitud> AgregarColaSolicitud(string tipo, object payload, string urlDestino = "");
        Task<List<DCO_ColaSolicitud>> AgregarColasSolicitudes(string tipo, object payload, List<string?>? urlsDestino = null);
    }
}
