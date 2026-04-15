using EvaCase.Application.Services;
using EvaCase.Domain.Entities;
using MediatR;
using TS.Result;

namespace EvaCase.Application.Features.Portfolios.RegisterPortfolio
{
    internal sealed class RegisterPortfolioCommandHandler(
        IApplicationDbContext context) : IRequestHandler<RegisterPortfolioCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(RegisterPortfolioCommand request, CancellationToken cancellationToken)
        {
            Portfolio portfolio = new()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedAt = DateTime.UtcNow
            };

            await context.Portfolios.AddAsync(portfolio, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return $"Portfolio '{portfolio.Name}' registered successfully (Id: {portfolio.Id})";
        }
    }
}
