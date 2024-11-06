using System.ComponentModel.DataAnnotations;

namespace NorebaseTask.Core.DTOs.Authentication;

public class UserRegisterDto
{
  [Required(ErrorMessage = "User name is required")]
  public string UserName { get; set; } = "";

  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Email address is invalid")]
  public string Email { get; set; } = "";

  [Required(ErrorMessage = "Password is required")]
  public string Password { get; set; } = "";

  public string Role { get; set; } = "";
}