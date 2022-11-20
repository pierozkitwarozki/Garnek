using FluentValidation;
using Garnek.Infrastructure.Constans;
using Garnek.Model.Dtos.Request;

namespace Garnek.Infrastructure.Validators;

public class AddPhrasesRequestValidator : AbstractValidator<AddPhrasesRequest>
{
    public int CategoriesNumber { get; } = 3;

    public AddPhrasesRequestValidator()
    {
        RuleFor(x => x.GameId)
            .NotNull()
            .WithMessage(ValidatorMessages.NullOrEmpty("GameId"))
            .NotEmpty()
            .WithMessage(ValidatorMessages.NullOrEmpty("GameId"));

        RuleFor(x => x.UserName)
            .NotNull()
            .WithMessage(ValidatorMessages.NullOrEmpty("UserName"))
            .NotEmpty()
            .WithMessage(ValidatorMessages.NullOrEmpty("UserName"));

        RuleFor(x => x.Phrases.Count())
            .Equal(CategoriesNumber)
            .WithMessage(ValidatorMessages.MustEqual("Phrase categories", CategoriesNumber));

        RuleForEach(x => x.Phrases)
            .Must(x => x.Value.Any())
            .WithMessage(ValidatorMessages.NullOrEmpty("Category phrases"));

        RuleFor(x => x.Phrases)
            .NotNull()
            .WithMessage(ValidatorMessages.NullOrEmpty("Phrases"))
            .NotEmpty()
            .WithMessage(ValidatorMessages.NullOrEmpty("Phrases"))
            .Must(DoDiffer)
            .WithMessage(ValidatorMessages.MustDiffer("Category phrases"));
    }

    private static bool DoDiffer(IDictionary<Guid, IEnumerable<string>> phrases)
    {
        var values = phrases.Values.SelectMany(x => x).ToList();
        return values.Distinct(StringComparer.CurrentCultureIgnoreCase).Count() == values.Count();
    }
}