using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using System.Diagnostics; // Necessário para o Activity
using WebApi.Interfaces; //

namespace WebApi.Services
{
    public class GameService
    {
        private readonly AppDbContext _context;
        private readonly IMessageBrokerClient _messageBroker; // NOVO: Dependência do Message Broker

        // Construtor atualizado para receber o Message Broker
        public GameService(AppDbContext context, IMessageBrokerClient messageBroker)
        {
            _context = context;
            _messageBroker = messageBroker;
        }

        // --- NOVO MÉTODO PARA INICIAR A COMPRA ---
        public async Task<bool> IniciarProcessoDeCompraAsync(Guid userId, Guid gameId)
        {
            var jogo = await _context.Games.FindAsync(gameId);
            if (jogo == null)
            {
                // Jogo não encontrado, não podemos continuar
                return false;
            }

            // 1. Criar o payload do nosso evento
            var evento = new 
            {
                UsuarioId = userId,
                JogoId = gameId,
                Valor = jogo.Price, // Supondo que seu modelo Game tem um campo Price
                Timestamp = DateTime.UtcNow
            };

            // 2. Preparar as propriedades da mensagem para o rastreamento
            var propriedadesDaMensagem = new Dictionary<string, object>();
            if (Activity.Current?.Id != null)
            {
                propriedadesDaMensagem["traceparent"] = Activity.Current.Id;
            }

            // 3. Publicar o evento no Message Broker com o "carimbo" do rastreamento
            await _messageBroker.PublicarEventoAsync("fila-de-compras", evento, propriedadesDaMensagem);

            return true;
        }


        // --- SEUS MÉTODOS ANTIGOS CONTINUAM AQUI ---
        // Eles ainda são úteis para outras funcionalidades

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