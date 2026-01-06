using GamesStore.Domain.Entities;
using GamesStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GamesStore.Infrastructure.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly AppDbContext _context;

        public PromotionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Promotion promotion)
        {
            await _context.Promotions.AddAsync(promotion);
        }

        public async Task<Promotion?> GetActiveByGameIdAsync(Guid gameId)
        {
            return await _context.Promotions
                .Where(p => p.GameId == gameId)
                .Where(p => p.StartsAt <= DateTime.UtcNow &&
                            p.EndsAt >= DateTime.UtcNow)
                .FirstOrDefaultAsync();
        }
    }
}
