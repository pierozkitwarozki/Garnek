using FluentValidation;
using Garnek.Infrastructure.Constans;
using Garnek.Model.Dtos.Request;

namespace Garnek.Infrastructure.Validators;

public class DrawTeamsRequestValidator : AbstractValidator<DrawTeamsRequest>
{
    public int MaxTeamsNumber { get; } = 4;
    public int MinTeamsNumber { get; } = 2;
    public DrawTeamsRequestValidator()
    {
        RuleFor(x => x.GameId)
            .NotEmpty()
            .WithMessage(ValidatorMessages.NullOrEmpty("GameId"))
            .NotNull()
            .WithMessage(ValidatorMessages.NullOrEmpty("GameId"));

        RuleFor(x => x.TeamsNumber)
            .GreaterThanOrEqualTo(MinTeamsNumber)
            .WithMessage(ValidatorMessages.MinLength("TeamsNumber", MinTeamsNumber))
            .LessThanOrEqualTo(MaxTeamsNumber)
            .WithMessage(ValidatorMessages.MaxLength("TeamsNumber", MaxTeamsNumber));

    }
}