using FluentValidation;
using Garnek.Infrastructure.Constans;
using Garnek.Model.Dtos.Request;

namespace Garnek.Infrastructure.Validators;

public class AddPhrasesRequestValidator : AbstractValidator<AddPhrasesRequest>
{
    public int CategoriesNumber { get; } = 3;
    public int MinPhraseLength { get; } = 2;
    public int MaxPhraseLength { get; } = 40;

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

        RuleForEach(x => GetPhraseValues(x.Phrases))
            .MinimumLength(MinPhraseLength)
            .OverridePropertyName("Phrase")
            .WithMessage(ValidatorMessages.MinLength("Phrase", MinPhraseLength))
            .MaximumLength(MaxPhraseLength)
            .OverridePropertyName("Phrase")
            .WithMessage(ValidatorMessages.MaxLength("Phrase", MaxPhraseLength));
    }

    private static bool DoDiffer(IDictionary<string, IEnumerable<string>> phrases)
    {
        var values = phrases.Values.SelectMany(x => x).ToList();
        return values.Distinct(StringComparer.CurrentCultureIgnoreCase).Count() == values.Count();
    }

    private static IEnumerable<string> GetPhraseValues(Dictionary<string, IEnumerable<string>> phrases)
    {
        return phrases.Values.SelectMany(phrase => phrase).ToList();
    }
}