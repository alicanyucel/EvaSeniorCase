using FluentValidation;

namespace EvaCase.Application.Features.Portfolios.RegisterPortfolio
{
    public sealed class RegisterPortfolioCommandValidator : AbstractValidator<RegisterPortfolioCommand>
    {
        public RegisterPortfolioCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Portfolio name is required")
                .MinimumLength(2).WithMessage("Portfolio name must be at least 2 characters");
        }
    }
}
