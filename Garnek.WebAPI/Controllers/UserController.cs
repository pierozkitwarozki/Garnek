using Garnek.Application.Services;
using Garnek.Model.Dtos.Request;
using Garnek.Model.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace Garnek.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    /// <summary>
    /// Adds players to initialize game and generate urls for phrase addition.
    /// At the moment from 6 to 10 players.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /User/InitializePlayers
    ///     {
    ///        "names": [
    ///             "Player1",
    ///             "Player2",
    ///             "Player3",
    ///             "Player4",
    ///             "Player5",
    ///             "Player6"
    ///             ]
    ///     }
    ///
    /// </remarks>
    /// <param name="request">Json as shown on example.</param>
    /// <response code="200">Correctly added users and generated urls for phrase addition.</response>
    /// <response code="422">Validation error.</response>
    /// <response code="400">Other error (ex. error parsing decoded gameId).</response>   
    [HttpPost(nameof(InitializePlayers))]
    [ProducesResponseType(typeof(InitializeGameResponse), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> InitializePlayers([FromBody] InitializeGameRequest request)
    {
        var response = await _userService.InitializeGameWithUsersAsync(request);
        return Ok(response);
    }
}

