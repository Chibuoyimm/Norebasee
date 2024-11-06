
using NorebaseTask.Core.DTOs.Authentication;
using NorebaseTask.Core.Common.Responses;

namespace NorebaseTask.Core.Interfaces.IServices;

public interface IAuthenticationService
{
  Task<Response<UserRegisterResponseDto>> RegisterAsync(UserRegisterDto userRegisterDto);
  Task<Response<string>> LoginAsync(UserLoginDto userLoginDto);
}