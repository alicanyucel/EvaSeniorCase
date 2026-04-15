using EvaCase.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EvaCase.Application.Features.Portfolios.GetAllPortfolios
{
    internal sealed class GetAllPortfoliosQueryHandler(
        IApplicationDbContext context) : IRequestHandler<GetAllPortfoliosQuery, Result<List<GetAllPortfoliosQueryResponse>>>
    {
        public async Task<Result<List<GetAllPortfoliosQueryResponse>>> Handle(GetAllPortfoliosQuery request, CancellationToken cancellationToken)
        {
            var portfolios = await context.Portfolios
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Select(p => new GetAllPortfoliosQueryResponse(
                    p.Id,
                    p.Name,
                    p.CreatedAt))
                .ToListAsync(cancellationToken);

            return portfolios;
        }
    }
}
