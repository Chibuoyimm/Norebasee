using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using NorebaseTask.Core;
using NorebaseTask.Core.Interfaces.IRepositories;
using NorebaseTask.Core.Interfaces.IServices;
using NorebaseTask.Core.DTOs.Authentication;
using NorebaseTask.Core.Common.Responses;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace NorebaseTask.Core.Services;

public class AuthenticationService : IAuthenticationService
{
  private readonly IUserRepository _userRepository;
  private readonly IJwtTokenService _tokenService;
  private readonly ILogger<AuthenticationService> _logger;

  public AuthenticationService(IUserRepository userRepository, IJwtTokenService tokenService, ILogger<AuthenticationService> logger)
  {
    _userRepository = userRepository;
    _tokenService = tokenService;
    _logger = logger;
  }

  public async Task<Response<UserRegisterResponseDto>> RegisterAsync(UserRegisterDto userRegisterDto)
  {
    var response = new Response<UserRegisterResponseDto>();
    try
    {
      var existingUser = await _userRepository.GetByEmailAsync(userRegisterDto.Email.ToLower());
      if (existingUser != null)
      {
        throw new InvalidOperationException("User with email already exists");
      }

      var newUser = new User
      {
        UserName = userRegisterDto.UserName,
        Email = userRegisterDto.Email.ToLower(),
        PasswordHash = BC.HashPassword(userRegisterDto.Password),
        Role = userRegisterDto.Role
      };

      var savedUser = await _userRepository.AddAsync(newUser);

      var userRegisterResponse = new UserRegisterResponseDto
      {
        Id = savedUser.Id.ToString(),
        UserName = savedUser.UserName,
        Role = savedUser.Role,
        CreatedAt = savedUser.CreatedAt.ToUniversalTime()
      };

      response.StatusCode = StatusCodes.Status200OK;
      response.Message = "User registered successfully.";
      response.Data = userRegisterResponse;

      return response;

    }
    catch (Exception ex)
    {
      // Handle specific exceptions
      _logger.LogError(ex, "An error occurred while registering user {UserName}", userRegisterDto.UserName);
      throw new Exception($"An error occurred during registration: {ex.Message}");
    }
  }

  public async Task<Response<string>> LoginAsync(UserLoginDto userLoginDto)
  {
    var response = new Response<string>();

    try
    {
      var user = await _userRepository.GetByEmailAsync(userLoginDto.Email.ToLower());

      // Verify that the user exists and the password is correct
      if (user == null || !BC.Verify(userLoginDto.Password, user.PasswordHash))
      {
        throw new UnauthorizedAccessException("Invalid email or password.");
      }

      // Generate a token for the authenticated user
      var token = _tokenService.GenerateToken(user);

      // Set the response data to the token
      response.StatusCode = StatusCodes.Status200OK;
      response.Message = "Login successful.";
      response.Data = token;

      return response;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occurred while logging in user with email {Email}", userLoginDto.Email);
      throw new Exception($"An error occurred during login: {ex.Message}");
    }
  }

}
