using GamesStore.Domain.Entities;
using GamesStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Xunit;
using GamesStore.Domain.Enums;
using GamesStore.Infrastructure.Data;

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
        public async Task Login_Should_Succeed_When_Credentials_Are_Valid()
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
    }
}
