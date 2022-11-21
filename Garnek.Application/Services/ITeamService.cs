using Garnek.Model.Dtos.Request;
using Garnek.Model.Dtos.Response;

namespace Garnek.Application.Services;

public interface ITeamService
{
    Task<DrawTeamsResponse> DrawTeamsForGameAsync(DrawTeamsRequest request);
}