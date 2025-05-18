using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TodoAppApi.Models
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(250)]
        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Navegación: una categoría puede tener muchas tareas
        public ICollection<Tarea> Tareas { get; set; }
    }
}
