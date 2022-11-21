using FluentValidation;
using Garnek.Application.Repositories;
using Garnek.Application.Services;
using Garnek.Infrastructure.Extensions;
using Garnek.Model.DatabaseModels;
using Garnek.Model.Dtos.Request;
using Garnek.Model.Dtos.Response;
using Garnek.Model.Exceptions;

namespace Garnek.Infrastructure.Services;

public class TeamService : ITeamService
{
    private readonly ITeamRepository _teamRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<DrawTeamsRequest> _validator;

    public TeamService(ITeamRepository teamRepository, IGameRepository gameRepository, 
        IUserRepository userRepository, IValidator<DrawTeamsRequest> validator)
    {
        _teamRepository = teamRepository;
        _gameRepository = gameRepository;
        _userRepository = userRepository;
        _validator = validator;
    }
    
    public async Task<DrawTeamsResponse> DrawTeamsForGameAsync(DrawTeamsRequest request)
    {
        await _validator.ValidateAndThrowAsync(request);
        
        var game = await _gameRepository.GetByIdAsync(request.GameId);

        if (game is null) throw new NotFoundException($"Game with id: {request.GameId} not found.");

        var gamePlayers = game.Users.ToList();
        
        if (!gamePlayers.Any()) throw new NotFoundException($"Game with id: {request.GameId} does not have any players.");
        if (gamePlayers.Any(x => x.TeamId != default))
            throw new InvalidOperationException($"One of users has already been assigned a team.");

        var shuffledPlayers = gamePlayers.OrderBy(p => Random.Shared.Next()).ToList();

        var teamsCount = request.TeamsNumber;
        var teamsToCreate = 
            Enumerable.Range(0, teamsCount)
                .Select(x => new Team())
                .ToList();

        await _teamRepository.AddRangeAsync(teamsToCreate);

        var teamsWithUsers = shuffledPlayers.Split(teamsCount).ToList();
        var teamsResponses = new List<TeamResponse>();

        for (var i = 0; i < teamsWithUsers.Count(); i++)
        {
            var teamId = teamsToCreate[i].Id;
            
            var usersWithTeamId = teamsWithUsers[i].Select(x =>
            {
                x.TeamId = teamId;
                return x;
            }).ToList();

            await _userRepository.UpdateRangeAsync(usersWithTeamId);
            var teamResponse = new TeamResponse(teamId, usersWithTeamId.Select(x => x.Name).ToList());
            teamsResponses.Add(teamResponse);
        }

        var response = new DrawTeamsResponse(request.GameId, teamsResponses);
        return response;
    }
}