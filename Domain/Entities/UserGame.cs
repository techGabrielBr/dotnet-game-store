namespace GamesStore.Domain.Entities
{
    public class UserGame
    {
        protected UserGame() { }

        public UserGame(Guid userId, Guid gameId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            GameId = gameId;
            AcquiredAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;

        public Guid GameId { get; private set; }
        public Game Game { get; private set; } = null!;

        public DateTime AcquiredAt { get; private set; }
    }
}
