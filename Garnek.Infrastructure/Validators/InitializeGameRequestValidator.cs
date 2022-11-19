using FluentValidation;
using Garnek.Model.Dtos.Request;

namespace Garnek.Infrastructure.Validators;

public class InitializeGameRequestValidator : AbstractValidator<InitializeGameRequest>
{
    public InitializeGameRequestValidator()
    {
        // Min number of players 6
        // Max number of players 10
        RuleFor(x => x.Names.Count())
            .NotNull()
            .GreaterThanOrEqualTo(6)
            .WithMessage("You must enter more than 5 users")
            .LessThanOrEqualTo(10)
            .WithMessage("You must enter less than 11 users");
        
        // Names must be unique
        RuleFor(x =>
                x.Names.Distinct(StringComparer.CurrentCultureIgnoreCase).Count() 
                == x.Names.Count())
            .Equal(true)
            .WithMessage("User names must be unique");
    }

}