using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RPG.API.DTOs.User;
using RPG.Core.Services;

namespace RPG.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register-user")]
    public async Task<ActionResult> RegisterUserAsync(CreateUserRequestDto newUser)
    {
        await _userService.CreateUser(newUser.Username, newUser.FirstName, newUser.LastName, newUser.Password,
            newUser.Email, newUser.Birthday);
        return Created();
    }

    [Authorize]
    [HttpPut("update-user")]
    public async Task<ActionResult> UpdateUserAsync(UserUpdateDto userUpdate)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }

        await _userService.UpdateUserAsync(userId, userUpdate.Username, userUpdate.Email);

        return Ok();
    }
}