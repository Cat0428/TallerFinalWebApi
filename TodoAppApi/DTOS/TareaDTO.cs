namespace TodoAppApi.DTOs
{
    public class TareaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string Categoria { get; set; }
        public string Estado { get; set; }
    }
}
