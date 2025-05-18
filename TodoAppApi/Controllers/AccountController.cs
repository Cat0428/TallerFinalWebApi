using Microsoft.AspNetCore.Mvc;
using TodoAppApi.DTOs;
using TodoAppApi.Models;
using TodoAppApi.Data;
using TodoAppApi.Services;
using BCrypt.Net;

namespace TodoAppApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AccountController(ApplicationDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        // POST: api/Account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistroDTO dto)
        {
            // Verificar si ya existe el nombre de usuario
            if (_context.Usuarios.Any(u => u.NombreUsuario == dto.NombreUsuario))
                return BadRequest("El nombre de usuario ya existe.");

            // Crear nuevo usuario con contraseña hasheada
            var usuario = new Usuario
            {
                NombreUsuario = dto.NombreUsuario,
                Correo = dto.Correo,
                ContrasenaHash = BCrypt.Net.BCrypt.HashPassword(dto.Contrasena),
                FechaCreacion = DateTime.UtcNow,
                Rol = UserRole.User
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuario registrado correctamente.");
        }

        // POST: api/Account/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.NombreUsuario == dto.NombreUsuario);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Contrasena, usuario.ContrasenaHash))
                return Unauthorized("Credenciales inválidas.");

            var token = _tokenService.GenerarToken(usuario);
            return Ok(new { token });
        }
    }
}
