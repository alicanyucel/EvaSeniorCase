using MediatR;
using TS.Result;

namespace EvaCase.Application.Features.Shares.GetAllShares
{
    public sealed record GetAllSharesQuery() : IRequest<Result<List<GetAllSharesQueryResponse>>>;

    public sealed record GetAllSharesQueryResponse(
        Guid Id,
        string Symbol,
        decimal CurrentPrice,
        DateTime UpdatedAt);
}
