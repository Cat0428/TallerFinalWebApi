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
        public string Nombre { get; set; } // <- unificado, nombre claro

        public ICollection<Tarea> Tareas { get; set; }
    }
}
