using EvaCase.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaCase.Application.Services
{
    public interface IApplicationDbContext
    {
        DbSet<Share> Shares { get; }
        DbSet<Portfolio> Portfolios { get; }
        DbSet<Trade> Trades { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
