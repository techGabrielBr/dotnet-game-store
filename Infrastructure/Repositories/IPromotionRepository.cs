using GamesStore.Domain.Entities;

namespace GamesStore.Infrastructure.Repositories
{
    public interface IPromotionRepository
    {
        Task AddAsync(Promotion promotion);
        Task<Promotion?> GetActiveByGameIdAsync(Guid gameId);
    }
}
