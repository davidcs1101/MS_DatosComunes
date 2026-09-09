namespace DCO.Aplicacion.Servicios.Interfaces
{
    public interface IProcesadorEventos
    {
        Task ProcesarAsync(string evento, string payload = "", string UrlDestino = "");
    }
}
