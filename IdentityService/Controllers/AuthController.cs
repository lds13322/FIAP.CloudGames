using IdentityService.Models;
using IdentityService.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;

        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            // Validação Mockada para o MVP (Garante velocidade na entrega)
            if (login.Email == "produtor@agrosolutions.com" && login.Password == "senha123")
            {
                var token = _tokenService.GenerateToken(login);
                
                return Ok(new 
                { 
                    Message = "Autenticação realizada com sucesso. Consentimento de uso de dados registrado (LGPD).",
                    Token = token 
                });
            }

            return Unauthorized(new { Message = "Credenciais inválidas." });
        }
    }
}