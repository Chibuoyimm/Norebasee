
namespace NorebaseTask.Core.Interfaces.IServices;

public interface IJwtTokenService
{
    string GenerateToken(User user);
}

