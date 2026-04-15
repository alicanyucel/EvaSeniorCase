using EvaCase.Domain.Entities;
using EvaCase.Domain.Enums;
using EvaCase.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EvaCase.Infrastructure.Seed
{
    public static class DataSeeder
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            if (context.Shares.Any()) return;

            // Seed Shares
            var shares = new List<Share>
            {
                new() { Id = Guid.NewGuid(), Symbol = "ABC", CurrentPrice = 25.50m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow.AddHours(-2) },
                new() { Id = Guid.NewGuid(), Symbol = "DEF", CurrentPrice = 42.75m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow.AddHours(-2) },
                new() { Id = Guid.NewGuid(), Symbol = "GHI", CurrentPrice = 18.00m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow.AddHours(-2) },
                new() { Id = Guid.NewGuid(), Symbol = "JKL", CurrentPrice = 99.99m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow.AddHours(-2) },
                new() { Id = Guid.NewGuid(), Symbol = "MNO", CurrentPrice = 55.10m, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow.AddHours(-2) }
            };
            context.Shares.AddRange(shares);
            context.SaveChanges();

            // Seed Portfolios (5 clients)
            var portfolios = new List<Portfolio>
            {
                new() { Id = Guid.NewGuid(), Name = "Alice Portfolio", CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), Name = "Bob Portfolio", CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), Name = "Charlie Portfolio", CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), Name = "Diana Portfolio", CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), Name = "Eve Portfolio", CreatedAt = DateTime.UtcNow }
            };
            context.Portfolios.AddRange(portfolios);
            context.SaveChanges();

            // Seed Trades – BUY and SELL transactions for each client
            var trades = new List<Trade>
            {
                // Alice: Buys ABC and DEF, sells some ABC
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[0].Id, ShareId = shares[0].Id, Type = TradeType.BUY, NoOfShares = 100, Price = 25.50m, CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[0].Id, ShareId = shares[1].Id, Type = TradeType.BUY, NoOfShares = 50,  Price = 42.75m, CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[0].Id, ShareId = shares[0].Id, Type = TradeType.SELL, NoOfShares = 30, Price = 26.00m, CreatedAt = DateTime.UtcNow },

                // Bob: Buys GHI and JKL, sells some GHI
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[1].Id, ShareId = shares[2].Id, Type = TradeType.BUY, NoOfShares = 200, Price = 18.00m, CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[1].Id, ShareId = shares[3].Id, Type = TradeType.BUY, NoOfShares = 10,  Price = 99.99m, CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[1].Id, ShareId = shares[2].Id, Type = TradeType.SELL, NoOfShares = 50, Price = 19.00m, CreatedAt = DateTime.UtcNow },

                // Charlie: Buys MNO and ABC
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[2].Id, ShareId = shares[4].Id, Type = TradeType.BUY, NoOfShares = 75,  Price = 55.10m, CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[2].Id, ShareId = shares[0].Id, Type = TradeType.BUY, NoOfShares = 120, Price = 25.50m, CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[2].Id, ShareId = shares[4].Id, Type = TradeType.SELL, NoOfShares = 25, Price = 56.00m, CreatedAt = DateTime.UtcNow },

                // Diana: Buys DEF and GHI, sells some DEF
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[3].Id, ShareId = shares[1].Id, Type = TradeType.BUY, NoOfShares = 80,  Price = 42.75m, CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[3].Id, ShareId = shares[2].Id, Type = TradeType.BUY, NoOfShares = 150, Price = 18.00m, CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[3].Id, ShareId = shares[1].Id, Type = TradeType.SELL, NoOfShares = 20, Price = 43.50m, CreatedAt = DateTime.UtcNow },

                // Eve: Buys JKL and MNO, sells some MNO
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[4].Id, ShareId = shares[3].Id, Type = TradeType.BUY, NoOfShares = 40,  Price = 99.99m, CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[4].Id, ShareId = shares[4].Id, Type = TradeType.BUY, NoOfShares = 60,  Price = 55.10m, CreatedAt = DateTime.UtcNow },
                new() { Id = Guid.NewGuid(), PortfolioId = portfolios[4].Id, ShareId = shares[4].Id, Type = TradeType.SELL, NoOfShares = 15, Price = 56.50m, CreatedAt = DateTime.UtcNow }
            };
            context.Trades.AddRange(trades);
            context.SaveChanges();
        }
    }
}
