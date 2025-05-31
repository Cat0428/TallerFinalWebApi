using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoAppApi.Data;
using TodoAppApi.DTOs;
using TodoAppApi.Models;

namespace TodoAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EstadoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get() => Ok(_context.Estados.ToList());

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] EstadoDTO dto)
        {
            var estado = new Estado { Nombre = dto.Nombre };
            _context.Estados.Add(estado);
            await _context.SaveChangesAsync();
            return Ok(estado);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] EstadoDTO dto)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null) return NotFound();
            estado.Nombre = dto.Nombre;
            await _context.SaveChangesAsync();
            return Ok(estado);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var estado = await _context.Estados.FindAsync(id);
            if (estado == null) return NotFound();
            _context.Estados.Remove(estado);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
