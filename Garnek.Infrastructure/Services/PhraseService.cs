using FluentValidation;
using Garnek.Application.Repositories;
using Garnek.Application.Services;
using Garnek.Infrastructure.Validators;
using Garnek.Model.DatabaseModels;
using Garnek.Model.Dtos.Request;
using Garnek.Model.Dtos.Response;
using Garnek.Model.Exceptions;

namespace Garnek.Infrastructure.Services;

public class PhraseService : IPhraseService
{
    private readonly IGameRepository _gameRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPhraseRepository _phraseRepository;
    private readonly IValidator<AddPhrasesRequest> _validator;
    private readonly IHashidsService _hashidsService;

    public PhraseService(IGameRepository gameRepository, 
        IUserRepository userRepository, 
        ICategoryRepository categoryRepository, 
        IPhraseRepository phraseRepository,
        IValidator<AddPhrasesRequest> validator,
        IHashidsService hashidsService)
    {
        _gameRepository = gameRepository;
        _userRepository = userRepository;
        _categoryRepository = categoryRepository;
        _phraseRepository = phraseRepository;
        _validator = validator;
        _hashidsService = hashidsService;
    }
    public async Task AddPhrasesAsync(AddPhrasesRequest request)
    {
        await _validator.ValidateAndThrowAsync(request);

        var decodedGameId = await ValidateEncodedGameIdAsync(request.GameId);
        
        var user = await ValidateUserNameAsync(decodedGameId, request.UserName);

        await AvoidDuplicateCreationAsync(user.Id);

        foreach (var phrase in request.Phrases)
        {
            var category = await _categoryRepository.GetByNameAsync(phrase.Key);
            if (category is null)
            {
                throw new NotFoundException($"Category with name: {phrase.Key} not found.");
            }
            var phrasesToAdd = phrase.Value.Select(p => new Phrase
            {
                CategoryId = category.Id,
                UserId = user.Id,
                Name = p,
            }).ToList();

            await _phraseRepository.AddRangeAsync(phrasesToAdd);
        }
    }

    public async Task<bool> CheckIfPhrasesCanBeAddedAsync(string encodedGameId, string userName)
    {
        var decodedGameId = await ValidateEncodedGameIdAsync(encodedGameId);
        
        var user = await ValidateUserNameAsync(decodedGameId, userName);

        await AvoidDuplicateCreationAsync(user.Id);

        return true;
    }

    public async Task<bool> AreAllPhrasesEnteredAsync(Guid gameId)
    {
        var game = await ValidateGameIdAsync(gameId);
        
        var castedValidator = _validator as AddPhrasesRequestValidator;

        var phrasesCount = castedValidator?.CategoriesNumber * castedValidator?.ExactNumberOfPhrasesPerCategory;

        var areAllPhrasesEntered = game.Users
            .All(x =>
                x.Phrases.Count() == phrasesCount);

        return areAllPhrasesEntered;
    }

    public async Task<GetPhrasesResponse> GetPhrasesForGameAsync(Guid gameId)
    {
        await ValidateGameIdAsync(gameId);

        var phrases = await _phraseRepository.GetByGameIdAsync(gameId);
        var phraseNames = phrases
            .Select(x => x.Name)
            .OrderBy(x => Random.Shared.Next(1000));

        return new GetPhrasesResponse(gameId, phraseNames);
    }

    private async Task AvoidDuplicateCreationAsync(Guid userId)
    {
        var createdPhrases = await _phraseRepository.GetByUserId(userId);

        if (createdPhrases.Any())
        {
            throw new InvalidOperationException("This user already entered phrases");
        }
    }
    private async Task<Guid> ValidateEncodedGameIdAsync(string encodedGameId)
    {
        var gameId = _hashidsService.DecodeToGuid(encodedGameId);
        var game = await _gameRepository.GetByIdAsync(gameId);
        if (game is null) throw new NotFoundException($"Game with id: {gameId} not found.");
        return gameId;
    }
    private async Task<Game> ValidateGameIdAsync(Guid gameId)
    {
        var game = await _gameRepository.GetByIdAsync(gameId);
        if (game is null) throw new NotFoundException($"Game with id: {gameId} not found.");
        return game;
    }
    private async Task<User> ValidateUserNameAsync(Guid gameId, string userName)
    {
        var user = await _userRepository.GetByNameAndGameIdAsync(gameId, userName);
        if (user is null) throw new NotFoundException($"User with name: {userName} not found.");
        return user;
    }
}