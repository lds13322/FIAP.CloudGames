namespace WebApi.Models
{
    public class UserGame
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid GameId { get; set; }
        public Game? Game { get; set; }
    }
}
