using Garnek.Application.Repositories;
using Garnek.Application.Services;
using Garnek.Infrastructure.DataAccess;
using Garnek.Model.DatabaseModels;
using Garnek.Model.Dtos;
using Garnek.Model.Dtos.Request;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

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
    
    [HttpPost("InitializePlayers")]
    public async Task<IActionResult> InitializePlayers([FromBody] InitializeGameRequest request)
    {
        try
        {
            var response = await _userService.InitializeGameWithUsersAsync(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

