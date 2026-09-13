namespace DCO.Aplicacion.Servicios.Interfaces
{
    public interface ISincronizadorMicroservicios
    {
        /// <summary>
        /// Este metodo se encarga de recibir los ids de las solicitudes de colas que van a ser sincronizadas y enviadas a TODOS los microservicios que requieran los datos comunes actualizados.
        /// Cada id representa una URL distinta que deberá ser notificada de la actualización de datos. El método no retorna un tipo, sólo activa el Job para que se notifique a cada microservicio en cuestión.
        /// </summary>
        /// <returns></returns>
        Task SincronizarTareasAsync(List<int> colasSolicitudIds);

        /// <summary>
        /// Este metodo se encarga de recibir un id de cola para que el proceso Job proceda a ejecutarlo de manera inmediata
        /// El método no retorna un tipo, sólo activa el Job para que se active la tarea correspondiente.
        /// </summary>
        /// <returns></returns>
        Task SincronizarTareaAsync(int colaSolicitudId);
    }
}
