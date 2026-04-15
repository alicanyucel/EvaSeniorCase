using EvaCase.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace EvaCase.Application.Features.Shares.UpdateSharePrice
{
    internal sealed class UpdateSharePriceCommandHandler(
        IApplicationDbContext context) : IRequestHandler<UpdateSharePriceCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateSharePriceCommand request, CancellationToken cancellationToken)
        {
            var share = await context.Shares
                .FirstOrDefaultAsync(s => s.Symbol == request.Symbol, cancellationToken);

            if (share is null)
            {
                return (400, $"Share with symbol '{request.Symbol}' is not registered");
            }

            if ((DateTime.UtcNow - share.UpdatedAt).TotalHours < 1)
            {
                return (400, "Share price can only be updated once per hour");
            }

            share.CurrentPrice = Math.Round(request.NewPrice, 2);
            share.UpdatedAt = DateTime.UtcNow;

            context.Shares.Update(share);
            await context.SaveChangesAsync(cancellationToken);

            return $"Share '{share.Symbol}' price updated to {share.CurrentPrice:F2}";
        }
    }
}
