using Garnek.Infrastructure.Constans;
using Garnek.Infrastructure.Validators;
using Garnek.Model.Dtos.Request;

namespace Garnek.Test.Unit.Validators;

public class AddPhrasesRequestValidatorTests
{
    private readonly AddPhrasesRequestValidator _validator;
    private readonly IList<Guid> _categoryIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
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
        var phrases = new Dictionary<Guid, IEnumerable<string>>
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
        var phrases = new Dictionary<Guid, IEnumerable<string>>
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
        var phrases = new Dictionary<Guid, IEnumerable<string>>
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
        var fourthCategoryId = Guid.NewGuid();
        var phrases = new Dictionary<Guid, IEnumerable<string>>
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
        var phrases = new Dictionary<Guid, IEnumerable<string>>
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
        var request = new AddPhrasesRequest(GameId, UserName, new Dictionary<Guid, IEnumerable<string>>());
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
        var phrases = new Dictionary<Guid, IEnumerable<string>>
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
}