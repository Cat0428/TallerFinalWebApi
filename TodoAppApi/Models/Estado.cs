using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TodoAppApi.Models
{
    public class Estado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreEstado { get; set; }

        // Navegación: un estado puede tener muchas tareas asociadas
        public ICollection<Tarea> Tareas { get; set; }
    }
}
