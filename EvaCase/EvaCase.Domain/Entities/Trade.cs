using EvaCase.Domain.Enums;

namespace EvaCase.Domain.Entities
{
    public sealed class Trade
    {
        public Guid Id { get; set; }
        public Guid PortfolioId { get; set; }
        public Guid ShareId { get; set; }
        public TradeType Type { get; set; }
        public int NoOfShares { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Portfolio Portfolio { get; set; } = default!;
        public Share Share { get; set; } = default!;
    }
}
