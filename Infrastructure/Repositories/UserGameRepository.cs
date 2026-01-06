using GamesStore.Domain.Entities;
using GamesStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GamesStore.Infrastructure.Repositories
{
    public class UserGameRepository(AppDbContext context) : IUserGameRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> ExistsAsync(Guid userId, Guid gameId)
            => await _context.UserGames
                .AnyAsync(ug => ug.UserId == userId && ug.GameId == gameId);

        public async Task AddAsync(UserGame userGame)
        {
            _context.UserGames.Add(userGame);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetUserGamesAsync(Guid userId)
            => await _context.UserGames
                .Include(ug => ug.Game)
                .Where(ug => ug.UserId == userId)
                .Select(ug => ug.Game)
                .ToListAsync();
    }
}
