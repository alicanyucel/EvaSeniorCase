using EvaCase.Application.Services;
using EvaCase.Domain.Entities;
using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EvaCase.Infrastructure.Context
{
    internal sealed class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Share> Shares => Set<Share>();
        public DbSet<Portfolio> Portfolios => Set<Portfolio>();
        public DbSet<Trade> Trades => Set<Trade>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUserRole<Guid>>();
            builder.Ignore<IdentityUserClaim<Guid>>();
        }
    }
}
