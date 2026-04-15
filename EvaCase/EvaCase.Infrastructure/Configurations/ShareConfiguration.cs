using EvaCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvaCase.Infrastructure.Configurations
{
    internal sealed class ShareConfiguration : IEntityTypeConfiguration<Share>
    {
        public void Configure(EntityTypeBuilder<Share> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Symbol)
                .HasColumnType("char(3)")
                .IsRequired();

            builder.HasIndex(s => s.Symbol)
                .IsUnique();

            builder.Property(s => s.CurrentPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
