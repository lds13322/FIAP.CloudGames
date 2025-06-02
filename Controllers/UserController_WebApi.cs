using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            var user = await _userService.RegisterAsync(dto.Name, dto.Email, dto.Password, dto.Role);
            if (user == null) return BadRequest("E-mail já cadastrado.");

            return Ok(new { user.Id, user.Name, user.Email, user.Role });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            var token = await _userService.LoginAsync(dto.Email, dto.Password);
            if (token == null) return Unauthorized("E-mail ou senha inválidos.");

            return Ok(new { token });
        }
    }

    public class UserRegisterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "User";
    }

    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
