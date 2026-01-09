using FluentAssertions;
using GamesStore.Domain.Entities;
using GamesStore.Domain.Enums;
using GamesStore.Infrastructure.Data;
using GamesStore.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace GamesStore.Tests.Auth
{
    public class LoginTests
    {
        private AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task Login_Should_Fail_When_User_Not_Exists()
        {
            var context = CreateContext();
            var repo = new UserRepository(context);

            var user = await repo.GetByEmailAsync("notfound@email.com");

            user.Should().BeNull();
        }

        [Fact]
        public async Task GetByEmailAsync_Should_Return_User_When_User_Exists()
        {
            var context = CreateContext();
            var repo = new UserRepository(context);

            var user = new User(
                "Teste",
                "teste@email.com",
                "HASH",
                UserRole.User
            );

            await repo.AddAsync(user);

            var result = await repo.GetByEmailAsync("teste@email.com");

            result.Should().NotBeNull();
            result!.Email.Should().Be("teste@email.com");
        }

        [Fact]
        public void VerifyPassword_Should_Succeed_With_Valid_Password()
        {
            var user = new User(
                "Teste",
                "teste@email.com",
                "",
                UserRole.User
            );

            var hasher = new PasswordHasher<User>();
            var password = "Senha@123";

            user.SetPassword(hasher.HashPassword(user, password));

            var result = hasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password
            );

            result.Should().Be(PasswordVerificationResult.Success);
        }
    }
}
