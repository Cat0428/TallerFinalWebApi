using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TodoAppApi.Models
{
    public enum UserRole
    {
        User,
        Admin
    }

    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string ContrasenaHash { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public UserRole Rol { get; set; } = UserRole.User;

        // Navegación: un usuario puede tener muchas tareas
        public ICollection<Tarea> Tareas { get; set; }
    }
}
