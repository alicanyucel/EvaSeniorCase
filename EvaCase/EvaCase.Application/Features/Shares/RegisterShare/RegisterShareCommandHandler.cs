using EvaCase.Application.Services;
using EvaCase.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EvaCase.Application.Features.Shares.RegisterShare
{
    internal sealed class RegisterShareCommandHandler(
        IApplicationDbContext context) : IRequestHandler<RegisterShareCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(RegisterShareCommand request, CancellationToken cancellationToken)
        {
            bool symbolExists = await context.Shares
                .AnyAsync(s => s.Symbol == request.Symbol, cancellationToken);

            if (symbolExists)
            {
                return (400, $"Share with symbol '{request.Symbol}' already exists");
            }

            Share share = new()
            {
                Id = Guid.NewGuid(),
                Symbol = request.Symbol,
                CurrentPrice = Math.Round(request.CurrentPrice, 2),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await context.Shares.AddAsync(share, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return $"Share '{share.Symbol}' registered successfully with price {share.CurrentPrice:F2}";
        }
    }
}
