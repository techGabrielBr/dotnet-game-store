using Xunit;
using FluentAssertions;

namespace GamesStore.Tests.Domain
{
    public class PasswordTests
    {
        [Theory]
        [InlineData("1234567")]
        [InlineData("password")]
        [InlineData("Password1")]
        [InlineData("12345678")]
        public void Password_Should_Be_Invalid_When_Weak(string password)
        {
            var result = IsValidPassword(password);

            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("Abc@1234")]
        [InlineData("Senha@2024")]
        [InlineData("Strong#99")]
        public void Password_Should_Be_Valid_When_Strong(string password)
        {
            var result = IsValidPassword(password);

            result.Should().BeTrue();
        }

        private static bool IsValidPassword(string password)
            => password.Length >= 8 &&
               password.Any(char.IsDigit) &&
               password.Any(char.IsLetter) &&
               password.Any(ch => !char.IsLetterOrDigit(ch));
    }
}
