using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Services
{
    public class GameService
    {
        private readonly AppDbContext _context;

        public GameService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Game> AddGameAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<bool> AddGameToLibraryAsync(Guid userId, Guid gameId)
        {
            var alreadyExists = await _context.UserGames
                .AnyAsync(ug => ug.UserId == userId && ug.GameId == gameId);

            if (alreadyExists) return false;

            var userGame = new UserGame
            {
                UserId = userId,
                GameId = gameId
            };

            _context.UserGames.Add(userGame);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Game>> GetLibraryAsync(Guid userId)
        {
            return await _context.UserGames
                .Where(ug => ug.UserId == userId)
                .Include(ug => ug.Game)
                .Select(ug => ug.Game!)
                .ToListAsync();
        }
    }
}
