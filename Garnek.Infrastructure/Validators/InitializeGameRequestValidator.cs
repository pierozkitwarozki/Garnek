using FluentValidation;
using Garnek.Infrastructure.Constans;
using Garnek.Model.Dtos.Request;

namespace Garnek.Infrastructure.Validators;

public class InitializeGameRequestValidator : AbstractValidator<InitializeGameRequest>
{
    public int MinNames { get; } = 6;
    public int MaxNames { get; } = 10;
    public int MinLength { get; } = 2;
    public int MaxLength { get; } = 20;
    public InitializeGameRequestValidator()
    {
        // Min number of players 6
        // Max number of players 10
        RuleFor(x => x.Names.Count())
            .NotEqual(0)
            .WithMessage(ValidatorMessages.NullOrEmpty("Names"))
            .GreaterThanOrEqualTo(MinNames)
            .WithMessage(ValidatorMessages.MinItemCount("Names", MinNames))
            .LessThanOrEqualTo(MaxNames)
            .WithMessage(ValidatorMessages.MaxItemCount("Names", MaxNames));
        
        // Names must be unique
        RuleFor(x =>
                x.Names.Distinct(StringComparer.CurrentCultureIgnoreCase).Count() 
                == x.Names.Count())
            .Equal(true)
            .WithMessage(ValidatorMessages.MustDiffer("Names"));

        RuleForEach(x => x.Names)
            .NotEmpty()
            .WithMessage(ValidatorMessages.NullOrEmpty("Name"))
            .NotNull()
            .WithMessage(ValidatorMessages.NullOrEmpty("Name"))
            .MinimumLength(MinLength)
            .WithMessage(ValidatorMessages.MinLength("Name", MinLength))
            .MaximumLength(MaxLength)
            .WithMessage(ValidatorMessages.MaxLength("Name", MaxLength));
    }
}