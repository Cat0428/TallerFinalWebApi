using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAppApi.Data;
using TodoAppApi.DTOs;
using TodoAppApi.Models;

namespace TodoAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("solo-admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult SoloAdmins()
        {
            return Ok("Bienvenido, administrador.");
        }

        [HttpGet("cualquiera")]
        [Authorize]
        public IActionResult ParaCualquiera()
        {
            return Ok("Hola, usuario autenticado.");
        }

        [HttpPut("cambiar-rol")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CambiarRol([FromBody] PromoverRolDTO dto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == dto.NombreUsuario);
            if (usuario == null)
                return NotFound("Usuario no encontrado.");

             if (!Enum.IsDefined(typeof(UserRole), dto.NuevoRol))
            return BadRequest("Rol inválido. Usa 0 (User) o 1 (Admin).");

            usuario.Rol = (UserRole)dto.NuevoRol;
            await _context.SaveChangesAsync();

            return Ok($"Rol del usuario '{usuario.NombreUsuario}' actualizado a '{usuario.Rol}'.");
        }
    }
}
