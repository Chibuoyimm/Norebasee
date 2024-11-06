
namespace NorebaseTask.Core.DTOs.Authentication;

public class UserRegisterResponseDto
{
  public string Id { get; set; } = "";
  public string UserName { get; set; } = "";
  public string Role { get; set; } = "";
  public DateTime CreatedAt { get; set; }
}