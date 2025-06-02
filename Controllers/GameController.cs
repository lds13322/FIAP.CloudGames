using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        // POST /api/game - apenas Admin
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddGame([FromBody] Game game)
        {
            var added = await _gameService.AddGameAsync(game);
            return Ok(added);
        }

        // GET /api/game - qualquer usuário autenticado
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetGames()
        {
            var games = await _gameService.GetAllGamesAsync();
            return Ok(games);
        }

        // POST /api/game/add/{gameId} - usuário adiciona à própria biblioteca
        [HttpPost("add/{gameId}")]
        [Authorize]
        public async Task<IActionResult> AddToLibrary(Guid gameId)
        {
            var userId = User.FindFirstValue("UserId");
            if (userId == null) return Unauthorized();

            var added = await _gameService.AddGameToLibraryAsync(Guid.Parse(userId), gameId);
            if (!added) return BadRequest("Jogo já está na biblioteca.");

            return Ok("Jogo adicionado com sucesso!");
        }

        // GET /api/game/library - lista biblioteca do usuário
        [HttpGet("library")]
        [Authorize]
        public async Task<IActionResult> GetLibrary()
        {
            var userId = User.FindFirstValue("UserId");
            if (userId == null) return Unauthorized();

            var library = await _gameService.GetLibraryAsync(Guid.Parse(userId));
            return Ok(library);
        }
    }
}
