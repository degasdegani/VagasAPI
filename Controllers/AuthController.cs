using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VagasAPI.Data;
using VagasAPI.DTOs;
using VagasAPI.Models;

namespace VagasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AppDbContext context, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        [HttpPost("registro")]
        public async Task<ActionResult> Registro(RegistroDto dto)
        {
            if (_context.Usuarios.Any(u => u.Email == dto.Email))
                return BadRequest("Email já cadastrado.");

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                Perfil = dto.Perfil
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Se for Candidato, cria o registro na tabela Candidatos automaticamente
            if (dto.Perfil == "Candidato")
            {
                var candidato = new Candidato
                {
                    UsuarioId = usuario.Id,
                    Telefone = "",
                    LinkedIn = ""
                };
                _context.Candidatos.Add(candidato);
                await _context.SaveChangesAsync();
            }

            _logger.LogInformation("Usuário registrado — Email: {Email}, Perfil: {Perfil}", dto.Email, dto.Perfil);

            return Ok(new { mensagem = "Usuário registrado com sucesso!" });
        }

        /// <summary>
        /// Realiza login e retorna o token JWT
        /// </summary>
        [HttpPost("login")]
        public ActionResult Login(LoginDto dto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == dto.Email);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(dto.Senha, usuario.SenhaHash))
                return Unauthorized("Email ou senha inválidos.");

            var token = GerarToken(usuario);

            _logger.LogInformation("Login realizado — Email: {Email}", dto.Email);

            return Ok(new { token });
        }

        private string GerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Perfil)
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}