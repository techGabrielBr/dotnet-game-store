using GamesStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamesStore.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Game> Games => Set<Game>();
        public DbSet<UserGame> UserGames => Set<UserGame>();
        public DbSet<Promotion> Promotions => Set<Promotion>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserGame>()
                .HasKey(ug => new { ug.UserId, ug.GameId });

            modelBuilder.Entity<UserGame>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.Games)
                .HasForeignKey(ug => ug.UserId);

            modelBuilder.Entity<UserGame>()
                .HasOne(ug => ug.Game)
                .WithMany()
                .HasForeignKey(ug => ug.GameId);

            modelBuilder.Entity<Game>()
                .HasOne(g => g.ActivePromotion)
                .WithOne()
                .HasForeignKey<Promotion>(p => p.GameId);
        }
    }
}
