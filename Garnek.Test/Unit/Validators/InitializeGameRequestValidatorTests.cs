using Garnek.Infrastructure.Constans;
using Garnek.Infrastructure.Validators;
using Garnek.Model.Dtos.Request;

namespace Garnek.Test.Unit.Validators;

public class InitializeGameRequestValidatorTests
{
    private readonly InitializeGameRequestValidator _validator;

    public InitializeGameRequestValidatorTests()
    {
        _validator = new InitializeGameRequestValidator();
    }

    [Theory]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "User8", "User9", "User10")]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "User8", "User9")]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "User8")]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7")]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6")]
    public async Task Validate_ShouldBeValid(params string[] userNames)
    {
        // Arrange
        var request = new InitializeGameRequest(userNames);

        // Act
        var validationResult = await _validator.ValidateAsync(request);

        // Assert
        Assert.True(validationResult.IsValid);
    }
    
    [Theory]
    [InlineData("User1", "User2", "User3", "User4", "User5")]
    [InlineData("User1", "User2", "User3", "User4")]
    public async Task Validate_ShouldNotBeValid_MinNames(params string[] userNames)
    {
        // Arrange
        var request = new InitializeGameRequest(userNames);
        var message = ValidatorMessages.MinItemCount("Names", _validator.MinNames);

        // Act
        var validationResult = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.True(validationResult.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Theory]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "User8", "User9", "User10", "User11", "User12")]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "User8", "User9", "User10", "User11")]
    public async Task Validate_ShouldNotBeValid_MaxNames(params string[] userNames)
    {
        // Arrange
        var request = new InitializeGameRequest(userNames);
        var message = ValidatorMessages.MaxItemCount("Names", _validator.MaxNames);

        // Act
        var validationResult = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.True(validationResult.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Fact]
    public async Task Validate_ShouldNotBeValid_NamesNull()
    {
        // Arrange
        var request = new InitializeGameRequest(Enumerable.Empty<string>());
        var message = ValidatorMessages.NullOrEmpty("Names");

        // Act
        var validationResult = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.True(validationResult.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Theory]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "User8", "")]
    [InlineData("User1", "User2", "User3", "User4", null, "User6", null, "User8")]
    public async Task Validate_ShouldNotBeValid_NullOrEmptyUserName(params string[] userNames)
    {
        // Arrange
        var request = new InitializeGameRequest(userNames);
        var message = ValidatorMessages.NullOrEmpty("Name");

        // Act
        var validationResult = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.True(validationResult.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Theory]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "User8", "U")]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "User8", "")]
    public async Task Validate_ShouldNotBeValid_MinNameLength(params string[] userNames)
    {
        // Arrange
        var request = new InitializeGameRequest(userNames);
        var message = ValidatorMessages.MinLength("Name", _validator.MinLength);

        // Act
        var validationResult = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.True(validationResult.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Theory]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "User8", "123123123123123123131313131231231321313113123131")]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "User8", "1231231231231231231313131312312313213131131231312132131")]
    public async Task Validate_ShouldNotBeValid_MaxNameLength(params string[] userNames)
    {
        // Arrange
        var request = new InitializeGameRequest(userNames);
        var message = ValidatorMessages.MaxLength("Name", _validator.MaxLength);

        // Act
        var validationResult = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.True(validationResult.Errors.Any(x => x.ErrorMessage == message));
    }
    
    [Theory]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User8", "User8")]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "uSeR7")]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "USER1")]
    [InlineData("User1", "User2", "User3", "User4", "User5", "User6", "User7", "user1")]
    public async Task Validate_ShouldNotBeValid_MustDiffer(params string[] userNames)
    {
        // Arrange
        var request = new InitializeGameRequest(userNames);
        var message = ValidatorMessages.MustDiffer("Names");

        // Act
        var validationResult = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(validationResult.IsValid);
        Assert.True(validationResult.Errors.Any(x => x.ErrorMessage == message));
    }
    
}