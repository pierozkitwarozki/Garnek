using Garnek.Application.Services;
using Garnek.Model.Dtos.Request;
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
    
    [HttpPost(nameof(DrawTeams))]
    public async Task<IActionResult> DrawTeams([FromBody]DrawTeamsRequest request)
    {
        var response = await _teamService.DrawTeamsForGameAsync(request);
        return Ok(response);
    }
}