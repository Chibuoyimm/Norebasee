using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using NorebaseTask.Core.Interfaces.IRepositories;

namespace NorebaseTask.Api.Middlewares;

public class JwtMiddleware
{
  private readonly RequestDelegate _next;

  public JwtMiddleware(RequestDelegate next)
  {
    _next = next;
  }

  public async Task Invoke(HttpContext context, IUserRepository userRepository)
  {
    // Check if the user is authenticated
    if (context.User.Identity?.IsAuthenticated == true)
    {
      // Extract the user ID claim from the authenticated JWT as a string
      var userIdClaim = context.User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
      if (userIdClaim != null)
      {
        // Pass the string user ID to the repository, which will handle GUID parsing
        context.Items["User"] = await userRepository.GetByIdAsync(userIdClaim);
      }
    }

    await _next(context);
  }
}

