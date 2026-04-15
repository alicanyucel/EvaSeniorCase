using FluentValidation;

namespace EvaCase.Application.Features.Shares.RegisterShare
{
    public sealed class RegisterShareCommandValidator : AbstractValidator<RegisterShareCommand>
    {
        public RegisterShareCommandValidator()
        {
            RuleFor(p => p.Symbol)
                .NotEmpty().WithMessage("Symbol is required")
                .Length(3).WithMessage("Symbol must be exactly 3 characters")
                .Matches("^[A-Z]{3}$").WithMessage("Symbol must be 3 uppercase letters");

            RuleFor(p => p.CurrentPrice)
                .GreaterThan(0).WithMessage("Price must be greater than 0")
                .PrecisionScale(18, 2, true).WithMessage("Price must have exactly 2 decimal digits");
        }
    }
}
