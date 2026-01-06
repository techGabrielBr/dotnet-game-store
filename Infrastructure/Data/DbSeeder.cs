using GamesStore.Domain.Entities;
using GamesStore.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace GamesStore.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static void SeedAdmin(AppDbContext context)
        {
            context.Database.EnsureCreated();

            const string adminEmail = "admin@gamesstore.com";

            var adminExists = context.Users.Any(u => u.Email == adminEmail);
            if (adminExists)
                return;

            var admin = new User(
                name: "Administrador",
                email: adminEmail,
                passwordHash: string.Empty,
                role: UserRole.Admin
            );

            var hasher = new PasswordHasher<User>();
            admin.SetPassword(
                hasher.HashPassword(admin, "Admin@123")
            );

            context.Users.Add(admin);
            context.SaveChanges();
        }
    }
}
