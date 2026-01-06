using GamesStore.Domain.Entities;
using GamesStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GamesStore.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Game?> GetByIdAsync(Guid id)
        {
            return await _context.Games
                .Include(g => g.ActivePromotion)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Game>> GetAllAsync()
        {
            return await _context.Games
                .Include(g => g.ActivePromotion)
                .ToListAsync();
        }

        public async Task AddAsync(Game game)
        {
            await _context.Games.AddAsync(game);
        }

        public async Task UpdateAsync(Game game)
        {
            _context.Games.Update(game);
            await Task.CompletedTask;
        }
    }
}
