using Garnek.Application.Repositories;
using Garnek.Infrastructure.DataAccess;
using Garnek.Model.DatabaseModels;
using Garnek.Model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Garnek.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    private readonly DatabaseContext _context;
    private readonly IUserRepository _userRepository;

    [HttpGet("/{userId:guid}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var user = await _userRepository.GetEntityByIdAsync(userId);

        return user is not null ? Ok(user) : NotFound();
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddUser([FromBody]AddUserRequest request)
    {
        var user = new User {
            Name = request.Name
        };
        var success = await _userRepository.AddEntityAsync(user);

        return success ? Ok(user) : BadRequest();
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUserName([FromQuery]UpdateUserRequest request)
    {
        var user = await _userRepository.GetEntityByIdAsync(request.UserId);

        if (user is null) return NotFound();

        user.Name = request.Name;

        var updatedUser = await _userRepository.UpdateEntityAsync(user);

        return updatedUser is not null ? Ok(user) : BadRequest();
    }
}

