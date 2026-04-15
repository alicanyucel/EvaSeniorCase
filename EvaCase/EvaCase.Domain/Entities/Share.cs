namespace EvaCase.Domain.Entities
{
    public sealed class Share
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 3 uppercase characters, unique identifier for the share.
        /// </summary>
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// Current price with exactly 2 decimal digits.
        /// </summary>
        public decimal CurrentPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Trade> Trades { get; set; } = new List<Trade>();
    }
}
