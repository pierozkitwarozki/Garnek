using FluentValidation;
using Garnek.Application.Repositories;
using Garnek.Application.Services;
using Garnek.Model.DatabaseModels;
using Garnek.Model.Dtos.Request;
using Garnek.Model.Dtos.Response;

namespace Garnek.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IValidator<InitializeGameRequest> _validator;
    private readonly IHashidsService _hashidsService;
    private const string BaseUrl = """https://garnek.phrase/""";

    public UserService(IUserRepository userRepository, IGameRepository gameRepository, 
        IValidator<InitializeGameRequest> validator, IHashidsService hashidsService)
    {
        _userRepository = userRepository;
        _gameRepository = gameRepository;
        _hashidsService = hashidsService;
        _validator = validator;
    }
    
    public async Task<InitializeGameResponse> InitializeGameWithUsersAsync(InitializeGameRequest request)
    {
        await _validator.ValidateAndThrowAsync(request);

        var game = new Game();
        
        await _gameRepository.AddEntityAsync(game);
        await AddUsersAsync(request.Names, game.Id);
        
        var encodedGameId = _hashidsService.EncodeGuid(game.Id);
        var urls = GeneratePhraseCreationUrls(request.Names, encodedGameId);

        return new InitializeGameResponse(encodedGameId, urls);
    }

    private static IEnumerable<string> GeneratePhraseCreationUrls(IEnumerable<string> userNames, string encodedId)
    {
        var urls = userNames.Select(x => $"{BaseUrl}{encodedId}/{x}").ToList();
        return urls;
    }

    private async Task AddUsersAsync(IEnumerable<string> userNames, Guid gameId)
    {
        var createdUsers = userNames.Select(x => new User
        {
            Name = x,
            GameId = gameId
        }).ToList();

        await _userRepository.AddEntitiesAsync(createdUsers);
    }
}