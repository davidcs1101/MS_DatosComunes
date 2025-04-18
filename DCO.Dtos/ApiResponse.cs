namespace DCO.Dtos
{
    public class ApiResponse<T>
    {
        public bool Correcto { get; set; }
        public string? Mensaje { get; set; }
        public T? Data { get; set; }
    }
}
