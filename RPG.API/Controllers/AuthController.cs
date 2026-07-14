using Microsoft.AspNetCore.Mvc;
using RPG.API.DTOs.Auth;
using RPG.Core.Interfaces;
using RPG.Core.Services;

namespace RPG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;
    private readonly UserService _userService;

    public AuthController(IPasswordHasher passwordHasher, ITokenProvider tokenProvider, UserService userService)
    {
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
        _userService = userService;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<string>> UserLogin(LoginRequestDto loginRequest)
    {
        var user = await _userService.GetUserByEmailAsync(loginRequest.Email);

        if (user == null || !_passwordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash))
            return Unauthorized("Email or password is wrong. Please try again");

        var jwt = _tokenProvider.GenerateToken(user);

        return Ok(new { token = jwt } );
    }
}