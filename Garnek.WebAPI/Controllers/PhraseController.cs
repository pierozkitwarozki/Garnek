using Garnek.Application.Services;
using Garnek.Model.Dtos.Request;
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

    [HttpPost]
    public async Task<IActionResult> AddPhrase(AddPhrasesRequest request)
    {
        await _phraseService.AddPhrasesAsync(request);
        return Ok();
    }
    
    [HttpGet("/{gameId:guid}")]
    public async Task<IActionResult> GetPhrasesForGame(Guid gameId)
    {
        var response = await _phraseService.GetPhrasesForGameAsync(gameId);
        return Ok(response);
    }
}