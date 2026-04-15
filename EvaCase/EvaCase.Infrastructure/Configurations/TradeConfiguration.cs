using EvaCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaCase.Infrastructure.Configurations
{
    internal sealed class TradeConfiguration : IEntityTypeConfiguration<Trade>
    {
        public void Configure(EntityTypeBuilder<Trade> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Type)
                .HasConversion<string>()
                .HasColumnType("varchar(4)")
                .IsRequired();

            builder.Property(t => t.NoOfShares)
                .IsRequired();

            builder.Property(t => t.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(t => t.Portfolio)
                .WithMany(p => p.Trades)
                .HasForeignKey(t => t.PortfolioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Share)
                .WithMany(s => s.Trades)
                .HasForeignKey(t => t.ShareId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
