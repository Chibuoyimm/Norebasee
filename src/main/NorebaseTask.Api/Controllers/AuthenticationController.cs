using Microsoft.AspNetCore.Mvc;
using NorebaseTask.Core.Interfaces.IRepositories;
using NorebaseTask.Core.Interfaces.IServices;
using NorebaseTask.Core.DTOs.Authentication;

namespace NorebaseTask.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenService _tokenService;
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IUserRepository userRepository, IJwtTokenService tokenService,
    IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _authenticationService = authenticationService;
    }

    // POST: api/authentication/register
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var response = await _authenticationService.RegisterAsync(userRegisterDto);
        return StatusCode(response.StatusCode, response);
    }

    // POST: api/authentication/login
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        if (!ModelState.IsValid)
            return StatusCode(StatusCodes.Status400BadRequest, ModelState);

        var response = await _authenticationService.LoginAsync(userLoginDto);
        return StatusCode(response.StatusCode, response);
    }
}
