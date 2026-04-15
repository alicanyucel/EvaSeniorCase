namespace EvaCase.Domain.Entities
{
    public sealed class Portfolio
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Trade> Trades { get; set; } = new List<Trade>();
    }
}
