using MediatR;
using TS.Result;

namespace EvaCase.Application.Features.Portfolios.GetAllPortfolios
{
    public sealed record GetAllPortfoliosQuery() : IRequest<Result<List<GetAllPortfoliosQueryResponse>>>;

    public sealed record GetAllPortfoliosQueryResponse(
        Guid Id,
        string Name,
        DateTime CreatedAt);
}
