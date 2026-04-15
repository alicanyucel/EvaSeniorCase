using MediatR;
using TS.Result;

namespace EvaCase.Application.Features.Portfolios.RegisterPortfolio
{
    public sealed record RegisterPortfolioCommand(
        string Name) : IRequest<Result<string>>;
}
