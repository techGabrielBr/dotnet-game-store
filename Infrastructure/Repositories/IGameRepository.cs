using GamesStore.Domain.Entities;

namespace GamesStore.Infrastructure.Repositories
{
    public interface IGameRepository
    {
        Task<Game?> GetByIdAsync(Guid id);
        Task<IEnumerable<Game>> GetAllAsync();
        Task AddAsync(Game game);
        Task UpdateAsync(Game game);
    }
}
