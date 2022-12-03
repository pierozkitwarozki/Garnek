using Garnek.Application.Services;
using Garnek.Model.Dtos.Request;
using Garnek.Model.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace Garnek.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamController(ITeamService teamService)
    {
        _teamService = teamService;
    }
    
    /// <summary>
    /// Draw teams for all users in specified game.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Team/DrawTeams
    ///     {
    ///        "gameId": "Decoded game Id",
    ///        "teamsNumber": 2
    ///     }
    ///
    /// </remarks>
    /// <param name="request">Json as shown on example.</param>
    /// <response code="200">Correctly assigned users to teams.</response>
    /// <response code="422">Validation error or player have already been assigned a team.</response>
    /// <response code="404">Player/game not found.</response>
    /// <response code="400">Other error (ex. error parsing decoded gameId).</response>   
    [HttpPost(nameof(DrawTeams))]
    [ProducesResponseType(typeof(DrawTeamsResponse), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> DrawTeams([FromBody]DrawTeamsRequest request)
    {
        var response = await _teamService.DrawTeamsForGameAsync(request);
        return Ok(response);
    }
}