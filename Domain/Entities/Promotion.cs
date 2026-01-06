namespace GamesStore.Domain.Entities
{
    public class Promotion
    {
        protected Promotion() { } // EF

        public Promotion(
            Guid gameId,
            decimal discountedPrice,
            DateTime startsAt,
            DateTime endsAt)
        {
            if (discountedPrice <= 0)
                throw new InvalidOperationException("Preço promocional inválido");

            if (endsAt <= startsAt)
                throw new InvalidOperationException("Período inválido");

            Id = Guid.NewGuid();
            GameId = gameId;
            DiscountedPrice = discountedPrice;
            StartsAt = startsAt;
            EndsAt = endsAt;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public Guid GameId { get; private set; }

        public decimal DiscountedPrice { get; private set; }
        public DateTime StartsAt { get; private set; }
        public DateTime EndsAt { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public bool IsActive =>
            DateTime.UtcNow >= StartsAt &&
            DateTime.UtcNow <= EndsAt;
    }
}
