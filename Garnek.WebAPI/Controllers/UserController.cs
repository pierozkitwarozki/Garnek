using Garnek.Application.Services;
using Garnek.Model.Dtos.Request;
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
    
    [HttpPost(nameof(InitializePlayers))]
    public async Task<IActionResult> InitializePlayers([FromBody] InitializeGameRequest request)
    {
        var response = await _userService.InitializeGameWithUsersAsync(request);
        return Ok(response);
    }
}

