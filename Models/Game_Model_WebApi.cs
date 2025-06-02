namespace WebApi.Models
{
    public class Game
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public decimal Price { get; set; }
        public ICollection<UserGame>? UserGames { get; set; }
    }
}
