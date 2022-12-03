using Garnek.Application.Services;
using Garnek.Model.Dtos.Request;
using Garnek.Model.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace Garnek.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PhraseController : ControllerBase
{
    private readonly IPhraseService _phraseService;

    public PhraseController(IPhraseService phraseService)
    {
        _phraseService = phraseService;
    }
    
    /// <summary>
    /// Creates phrases records for a player.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /Phrase
    ///     {
    ///        "gameId": "Encoded Game Id",
    ///        "userName": "Player1",
    ///        "phrases": {
    ///             "people": ["phrase1", "phrase2", "phrase3"],
    ///             "places": ["phrase4", "phrase5", "phrase6"],
    ///             "things": ["phrase7", "phrase8", "phrase9"],
    ///         }
    ///     }
    ///
    /// </remarks>
    /// <param name="request">Json as shown on example.</param>
    /// <response code="200">Nothing. Phrases added successfully.</response>
    /// <response code="422">Validation error or phrases have already been put.</response>
    /// <response code="404">Player/game not found.</response>
    /// <response code="400">Other error (ex. error parsing decoded gameId).</response>   
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPhrase([FromBody]AddPhrasesRequest request)
    {
        await _phraseService.AddPhrasesAsync(request);
        return Ok();
    }
    
    /// <summary>
    /// Gets all phrases for a game in a random order.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /Phrase/{gameId}
    ///
    /// </remarks>
    /// <param name="gameId">Decoded gameId (for now it's guid).</param>
    /// <response code="200">List of all phrases for given game.</response>
    /// <response code="404">Game not found.</response>
    /// <response code="400">Other unexpected error.</response>   
    [HttpGet("/{gameId:guid}")]
    [ProducesResponseType(typeof(GetPhrasesResponse), StatusCodes.Status200OK, "application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPhrasesForGame(Guid gameId)
    {
        var response = await _phraseService.GetPhrasesForGameAsync(gameId);
        return Ok(response);
    }

    /// <summary>
    /// Checks if player can add phrases to game and returns true if so.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /Phrase/{gameId}/{userName}
    ///
    /// </remarks>
    /// <param name="gameId">Encoded gameId.</param>
    /// <param name="userName">Name of a player. Case insensitive.</param>
    /// <response code="200">True. Phrases can be added.</response>
    /// <response code="404">Game/Player not found.</response>
    /// <response code="422">Validation error/player already entered phrases.</response>
    /// <response code="400">Other unexpected error.</response> 
    [HttpGet(nameof(CanBeAdded) + "/{gameId}/{userName}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CanBeAdded(string gameId, string userName)
    {
        var response = await _phraseService.CheckIfPhrasesCanBeAddedAsync(gameId, userName);
        return Ok(response);
    }
    
    /// <summary>
    /// Checks if all phrases have been entered and game is ready to be started.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     GET /Phrase/{gameId}
    ///
    /// </remarks>
    /// <param name="gameId">Encoded gameId.</param>
    /// <response code="200">True. Game is ready.</response>
    /// <response code="404">Game/Player not found.</response>
    /// <response code="422">Validation error/player already entered phrases.</response>
    /// <response code="400">Other unexpected error.</response> 
    [HttpGet(nameof(AllEntered) + "/{gameId:guid}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AllEntered(Guid gameId)
    {
        var response = await _phraseService.AreAllPhrasesEnteredAsync(gameId);
        return Ok(response);
    }
}