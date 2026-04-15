using EvaCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaCase.Infrastructure.Configurations
{
    internal sealed class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
