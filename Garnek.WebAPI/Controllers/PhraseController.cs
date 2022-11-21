using Garnek.Application.Services;
using Garnek.Model.Dtos.Request;
using Garnek.Model.Exceptions;
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
    
    [HttpGet("/{gameId}")]
    public async Task<IActionResult> GetPhraseForGame(string gameId)
    {
        var response = await _phraseService.GetPhrasesForGameAsync(gameId);
        return Ok(response);
    }
}