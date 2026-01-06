using GamesStore.Domain.Entities;

namespace GamesStore.Infrastructure.Repositories
{
    public interface IUserGameRepository
    {
        Task<bool> ExistsAsync(Guid userId, Guid gameId);
        Task AddAsync(UserGame userGame);
        Task<IEnumerable<Game>> GetUserGamesAsync(Guid userId);
    }
}
