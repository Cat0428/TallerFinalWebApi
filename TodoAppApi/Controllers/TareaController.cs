using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TodoAppApi.Data;
using TodoAppApi.DTOs;
using TodoAppApi.Models;

namespace TodoAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TareaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TareaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tarea
        [HttpGet]
        public async Task<IActionResult> GetMisTareas()
        {
            var nombreUsuario = User.FindFirstValue(ClaimTypes.Name);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null) return Unauthorized();

            var tareas = await _context.Tareas
                .Where(t => t.UsuarioId == usuario.Id)
                .Include(t => t.Categoria)
                .Include(t => t.Estado)
                .Select(t => new TareaDTO
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descripcion = t.Descripcion,
                    FechaVencimiento = t.FechaVencimiento.Value,

                    Categoria = t.Categoria.Nombre,
                    Estado = t.Estado.Nombre
                })
                .ToListAsync();

            return Ok(tareas);
        }

        // POST: api/Tarea
        [HttpPost]
        public async Task<IActionResult> CrearTarea([FromBody] TareaCreateDTO dto)
        {
            var nombreUsuario = User.FindFirstValue(ClaimTypes.Name);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null) return Unauthorized();

            var tarea = new Tarea
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                FechaVencimiento = dto.FechaVencimiento,
                CategoriaId = dto.CategoriaId,
                EstadoId = dto.EstadoId,
                UsuarioId = usuario.Id,
                FechaCreacion = DateTime.UtcNow
            };

            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();

            return Ok("Tarea creada exitosamente.");
        }

        // PUT: api/Tarea/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarTarea(int id, [FromBody] TareaCreateDTO dto)
        {
            var nombreUsuario = User.FindFirstValue(ClaimTypes.Name);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null) return Unauthorized();

            var tarea = await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuario.Id);

            if (tarea == null) return NotFound("Tarea no encontrada.");

            tarea.Titulo = dto.Titulo;
            tarea.Descripcion = dto.Descripcion;
            tarea.FechaVencimiento = dto.FechaVencimiento;
            tarea.CategoriaId = dto.CategoriaId;
            tarea.EstadoId = dto.EstadoId;

            await _context.SaveChangesAsync();

            return Ok("Tarea actualizada correctamente.");
        }

        // DELETE: api/Tarea/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTarea(int id)
        {
            var nombreUsuario = User.FindFirstValue(ClaimTypes.Name);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null) return Unauthorized();

            var tarea = await _context.Tareas
                .FirstOrDefaultAsync(t => t.Id == id && t.UsuarioId == usuario.Id);

            if (tarea == null) return NotFound("Tarea no encontrada.");

            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();

            return Ok("Tarea eliminada correctamente.");
        }
    }
}
