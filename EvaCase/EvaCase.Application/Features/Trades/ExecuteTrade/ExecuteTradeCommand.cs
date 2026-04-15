using MediatR;
using TS.Result;

namespace EvaCase.Application.Features.Trades.ExecuteTrade
{
    public sealed record ExecuteTradeCommand(
        Guid PortfolioId,
        string ShareSymbol,
        string Type,
        int NoOfShares) : IRequest<Result<string>>;
}
