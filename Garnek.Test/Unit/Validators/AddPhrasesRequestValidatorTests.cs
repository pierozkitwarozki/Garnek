using System.Text;
using Garnek.Infrastructure.Constans;
using Garnek.Infrastructure.Validators;
using Garnek.Model.Dtos.Request;

namespace Garnek.Test.Unit.Validators;

public class AddPhrasesRequestValidatorTests
{
    private readonly AddPhrasesRequestValidator _validator;
    private readonly IList<string> _categoryIds = new List<string> { "People", "Places", "Things" };
    private const string UserName = "Test";
    private const string GameId = "ABDFEFAACXER1209DAS";

    public AddPhrasesRequestValidatorTests()
    {
        _validator = new AddPhrasesRequestValidator();
    }

    [Fact]
    public async Task Validate_ShouldBeValid()
    {
        // Arrange
        var phrases = new Dictionary<string, IEnumerable<string>>
        {
            { _categoryIds[0], new[] { "Phrase1", "Phrase2", "Phrase3" } },
            { _categoryIds[1], new[] { "Phrase4", "Phrase5", "Phrase6" } },
            { _categoryIds[2], new[] { "Phrase7", "Phrase8", "Phrase9" } }
        };
        var request = new AddPhrasesRequest(GameId, UserName, phrases);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.True(result.IsValid);
    }
    
    [Fact]
    public async Task Validate_ShouldNotBeValid_EmptyGameId()
    {
        // Arrange
        var phrases = new Dictionary<string, IEnumerable<string>>
        {
            { _categoryIds[0], new[] { "Phrase1", "Phrase2", "Phrase3" } },
            { _categoryIds[1], new[] { "Phrase4", "Phrase5", "Phrase6" } },
            { _categoryIds[2], new[] { "Phrase7", "Phrase8", "Phrase9" } }
        };
        var request = new AddPhrasesRequest(string.Empty, UserName, phrases);
        var message = ValidatorMessages.NullOrEmpty("GameId");

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Fact]
    public async Task Validate_ShouldNotBeValid_EmptyUserName()
    {
        // Arrange
        var phrases = new Dictionary<string, IEnumerable<string>>
        {
            { _categoryIds[0], new[] { "Phrase1", "Phrase2", "Phrase3" } },
            { _categoryIds[1], new[] { "Phrase4", "Phrase5", "Phrase6" } },
            { _categoryIds[2], new[] { "Phrase7", "Phrase8", "Phrase9" } }
        };
        var request = new AddPhrasesRequest(GameId, string.Empty, phrases);
        var message = ValidatorMessages.NullOrEmpty("UserName");

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Fact]
    public async Task Validate_ShouldNotBeValid_WrongCategoriesNumber()
    {
        // Arrange
        var fourthCategoryId = "NonExisting";
        var phrases = new Dictionary<string, IEnumerable<string>>
        {
            { _categoryIds[0], new[] { "Phrase1", "Phrase2", "Phrase3" } },
            { _categoryIds[1], new[] { "Phrase4", "Phrase5", "Phrase6" } },
            { _categoryIds[2], new[] { "Phrase7", "Phrase8", "Phrase9" } },
            { fourthCategoryId, new[] { "Phrase10", "Phrase11", "Phrase12" } }
        };
        var request = new AddPhrasesRequest(GameId, UserName, phrases);
        var message = ValidatorMessages.MustEqual("Phrase categories", _validator.CategoriesNumber);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Fact]
    public async Task Validate_ShouldNotBeValid_NoPhrasesInCategory()
    {
        // Arrange
        var phrases = new Dictionary<string, IEnumerable<string>>
        {
            { _categoryIds[0], new[] { "Phrase1", "Phrase2", "Phrase3" } },
            { _categoryIds[1], new[] { "Phrase4", "Phrase5", "Phrase6" } },
            { _categoryIds[2], Enumerable.Empty<string>() }
        };
        var request = new AddPhrasesRequest(GameId, UserName, phrases);
        var message = ValidatorMessages.NullOrEmpty("Category phrases");

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Fact]
    public async Task Validate_ShouldNotBeValid_NoPhrases()
    {
        // Arrange
        var request = new AddPhrasesRequest(GameId, UserName, new Dictionary<string, IEnumerable<string>>());
        var message = ValidatorMessages.NullOrEmpty("Phrases");

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Theory]
    [InlineData("Phrase1", "phrase1")]
    [InlineData("PHRASE1", "PHRaSe1")]
    [InlineData("phrase1", "PHraSe1")]
    public async Task Validate_ShouldNotBeValid_DuplicatePhrases(params string[] duplicatePhrases)
    {
        // Arrange
        var phrases = new Dictionary<string, IEnumerable<string>>
        {
            { _categoryIds[0], new[] { duplicatePhrases[0], "Phrase2", "Phrase3" } },
            { _categoryIds[1], new[] { "Phrase4", "Phrase5", duplicatePhrases[1] } },
            { _categoryIds[2], new[] { "Phrase7", "Phrase8", "Phrase9" } }
        };
        var request = new AddPhrasesRequest(GameId, UserName, phrases);
        var message = ValidatorMessages.MustDiffer("Category phrases");

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Fact]
    public async Task Validate_ShouldNotBeValid_TooShortPhrase()
    {
        // Arrange
        const string shortPhrase = "1";
        var phrases = new Dictionary<string, IEnumerable<string>>
        {
            { _categoryIds[0], new[] { shortPhrase, "Phrase2", "Phrase3" } },
            { _categoryIds[1], new[] { "Phrase4", "Phrase5", "Phrase6" } },
            { _categoryIds[2], new[] { "Phrase7", "Phrase8", "Phrase9" } }
        };
        var request = new AddPhrasesRequest(GameId, UserName, phrases);
        var message = ValidatorMessages.MinLength("Phrase", _validator.MinPhraseLength);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Fact]
    public async Task Validate_ShouldNotBeValid_TooLongPhrase()
    {
        // Arrange
        var sb = new StringBuilder();
        Enumerable.Range(0, 50).Select(x => x.ToString()).ToList().ForEach(x => sb.Append(x));
        var tooLongPhrase = sb.ToString();
        
        var phrases = new Dictionary<string, IEnumerable<string>>
        {
            { _categoryIds[0], new[] { tooLongPhrase, "Phrase2", "Phrase3" } },
            { _categoryIds[1], new[] { "Phrase4", "Phrase5", "Phrase6" } },
            { _categoryIds[2], new[] { "Phrase7", "Phrase8", "Phrase9" } }
        };
        var request = new AddPhrasesRequest(GameId, UserName, phrases);
        var message = ValidatorMessages.MaxLength("Phrase", _validator.MaxPhraseLength);

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Any(x => x.ErrorMessage == message));
    }
}