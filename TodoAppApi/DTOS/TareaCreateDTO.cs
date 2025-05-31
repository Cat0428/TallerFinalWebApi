namespace TodoAppApi.DTOs
{
    public class TareaCreateDTO
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int CategoriaId { get; set; }
        public int EstadoId { get; set; }
    }
}
