using EvaCase.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EvaCase.Application.Features.Shares.GetAllShares
{
    internal sealed class GetAllSharesQueryHandler(
        IApplicationDbContext context) : IRequestHandler<GetAllSharesQuery, Result<List<GetAllSharesQueryResponse>>>
    {
        public async Task<Result<List<GetAllSharesQueryResponse>>> Handle(GetAllSharesQuery request, CancellationToken cancellationToken)
        {
            var shares = await context.Shares
                .AsNoTracking()
                .OrderBy(s => s.Symbol)
                .Select(s => new GetAllSharesQueryResponse(
                    s.Id,
                    s.Symbol,
                    s.CurrentPrice,
                    s.UpdatedAt))
                .ToListAsync(cancellationToken);

            return shares;
        }
    }
}
