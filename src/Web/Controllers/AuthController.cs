// Controlador de Autenticación - Login con JWT

using Aplicacion.Servicios;
using Dominio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Protección global para todos los controladores por defecto
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly IConfiguration _config;

        public AuthController(UsuarioService usuarioService, IConfiguration config)
        {
            _usuarioService = usuarioService;
            _config = config;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = (await _usuarioService.ListarAsync())
                .FirstOrDefault(u => u.NombreUsuario == login.Usuario && u.ClaveHash == login.Clave);

            if (usuario == null)
                return Unauthorized("Credenciales inválidas");

            var token = GenerarToken(usuario);
            return Ok(new { token });
        }

        [HttpGet("perfil")]
        public IActionResult Perfil()
        {
            var id = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            var nombre = User.Claims.FirstOrDefault(c => c.Type == "nombre")?.Value;
            return Ok(new { id, nombre });
        }

        private string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.NombreUsuario),
                new Claim("id", usuario.Id.ToString()),
                new Claim("nombre", usuario.NombreCompleto),
            };

            var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class LoginRequest
        {
            [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
            public string Usuario { get; set; } = string.Empty;

            [Required(ErrorMessage = "La clave es obligatoria")]
            public string Clave { get; set; } = string.Empty;
        }
    }
}
