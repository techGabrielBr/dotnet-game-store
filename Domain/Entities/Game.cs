namespace GamesStore.Domain.Entities
{
    public class Game
    {
        protected Game() { } // EF

        public Game(string title, decimal basePrice)
        {
            Id = Guid.NewGuid();
            Title = title;
            BasePrice = basePrice;
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; } = null!;
        public decimal BasePrice { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Promotion? ActivePromotion { get; private set; }

        public decimal CurrentPrice =>
            ActivePromotion is not null
                ? ActivePromotion.DiscountedPrice
                : BasePrice;

        public void UpdateBasePrice(decimal newPrice)
        {
            if (newPrice <= 0)
                throw new InvalidOperationException("Preço inválido");

            BasePrice = newPrice;
        }

        public void ApplyPromotion(Promotion promotion)
        {
            if (promotion.GameId != Id)
                throw new InvalidOperationException("Promoção inválida");

            ActivePromotion = promotion;
        }

        public void RemovePromotion()
        {
            ActivePromotion = null;
        }
    }
}
