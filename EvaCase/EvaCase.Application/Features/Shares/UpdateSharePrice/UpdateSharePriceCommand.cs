using MediatR;
using TS.Result;

namespace EvaCase.Application.Features.Shares.UpdateSharePrice
{
    public sealed record UpdateSharePriceCommand(
        string Symbol,
        decimal NewPrice) : IRequest<Result<string>>;
}
