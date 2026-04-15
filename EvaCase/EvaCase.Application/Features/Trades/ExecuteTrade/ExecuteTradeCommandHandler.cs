using EvaCase.Application.Services;
using EvaCase.Domain.Entities;
using EvaCase.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EvaCase.Application.Features.Trades.ExecuteTrade
{
    internal sealed class ExecuteTradeCommandHandler(
        IApplicationDbContext context) : IRequestHandler<ExecuteTradeCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(ExecuteTradeCommand request, CancellationToken cancellationToken)
        {
            var portfolio = await context.Portfolios
                .FirstOrDefaultAsync(p => p.Id == request.PortfolioId, cancellationToken);

            if (portfolio is null)
            {
                return (400, "Portfolio is not registered");
            }

            var share = await context.Shares
                .FirstOrDefaultAsync(s => s.Symbol == request.ShareSymbol, cancellationToken);

            if (share is null)
            {
                return (400, $"Share with symbol '{request.ShareSymbol}' is not registered");
            }

            TradeType tradeType = Enum.Parse<TradeType>(request.Type);

            if (tradeType == TradeType.BUY)
            {
                return await HandleBuy(portfolio, share, request.NoOfShares, cancellationToken);
            }
            else
            {
                return await HandleSell(portfolio, share, request.NoOfShares, cancellationToken);
            }
        }

        private async Task<Result<string>> HandleBuy(Portfolio portfolio, Share share, int noOfShares, CancellationToken cancellationToken)
        {
            Trade trade = new()
            {
                Id = Guid.NewGuid(),
                PortfolioId = portfolio.Id,
                ShareId = share.Id,
                Type = TradeType.BUY,
                NoOfShares = noOfShares,
                Price = share.CurrentPrice,
                CreatedAt = DateTime.UtcNow
            };

            await context.Trades.AddAsync(trade, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return $"BUY successful: {noOfShares} shares of {share.Symbol} at {share.CurrentPrice:F2} for portfolio '{portfolio.Name}'";
        }

        private async Task<Result<string>> HandleSell(Portfolio portfolio, Share share, int noOfShares, CancellationToken cancellationToken)
        {
            var trades = await context.Trades
                .Where(t => t.PortfolioId == portfolio.Id && t.ShareId == share.Id)
                .ToListAsync(cancellationToken);

            int totalBought = trades.Where(t => t.Type == TradeType.BUY).Sum(t => t.NoOfShares);
            int totalSold = trades.Where(t => t.Type == TradeType.SELL).Sum(t => t.NoOfShares);
            int availableShares = totalBought - totalSold;

            if (availableShares <= 0)
            {
                return (400, $"Share '{share.Symbol}' is not in the portfolio");
            }

            if (availableShares < noOfShares)
            {
                return (400, $"Insufficient shares. Available: {availableShares}, Requested: {noOfShares}");
            }

            Trade trade = new()
            {
                Id = Guid.NewGuid(),
                PortfolioId = portfolio.Id,
                ShareId = share.Id,
                Type = TradeType.SELL,
                NoOfShares = noOfShares,
                Price = share.CurrentPrice,
                CreatedAt = DateTime.UtcNow
            };

            await context.Trades.AddAsync(trade, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return $"SELL successful: {noOfShares} shares of {share.Symbol} at {share.CurrentPrice:F2} for portfolio '{portfolio.Name}'";
        }
    }
}
