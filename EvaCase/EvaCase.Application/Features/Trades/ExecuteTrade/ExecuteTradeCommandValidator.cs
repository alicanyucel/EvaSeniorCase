using FluentValidation;

namespace EvaCase.Application.Features.Trades.ExecuteTrade
{
    public sealed class ExecuteTradeCommandValidator : AbstractValidator<ExecuteTradeCommand>
    {
        public ExecuteTradeCommandValidator()
        {
            RuleFor(p => p.PortfolioId)
                .NotEmpty().WithMessage("PortfolioId is required");

            RuleFor(p => p.ShareSymbol)
                .NotEmpty().WithMessage("ShareSymbol is required")
                .Length(3).WithMessage("ShareSymbol must be exactly 3 characters")
                .Matches("^[A-Z]{3}$").WithMessage("ShareSymbol must be 3 uppercase letters");

            RuleFor(p => p.Type)
                .NotEmpty().WithMessage("Trade type is required")
                .Must(t => t == "BUY" || t == "SELL").WithMessage("Type must be 'BUY' or 'SELL'");

            RuleFor(p => p.NoOfShares)
                .GreaterThan(0).WithMessage("Number of shares must be greater than 0");
        }
    }
}
