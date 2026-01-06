using GamesStore.Domain.Enums;

namespace GamesStore.Domain.Entities
{
    public class User
    {
        protected User() { } // EF Core

        public User(
            string name,
            string email,
            string passwordHash,
            UserRole role)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
            Games = new List<UserGame>();
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public UserRole Role { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ICollection<UserGame> ?Games { get; private set; }

        public void SetPassword(string hash)
        {
            PasswordHash = hash;
        }

        public void Update(string name, string email, UserRole role)
        {
            Name = name;
            Email = email;
            Role = role;
        }
    }
}
