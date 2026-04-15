using MediatR;
using TS.Result;

namespace EvaCase.Application.Features.Shares.RegisterShare
{
    public sealed record RegisterShareCommand(
        string Symbol,
        decimal CurrentPrice) : IRequest<Result<string>>;
}
