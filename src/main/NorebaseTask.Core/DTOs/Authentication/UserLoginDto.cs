using System.ComponentModel.DataAnnotations;

namespace NorebaseTask.Core.DTOs.Authentication;

public class UserLoginDto
{
  [Required(ErrorMessage = "Email is required")]
  [EmailAddress(ErrorMessage = "Email address is invalid")]
  public string Email { get; set; } = "";

  [Required(ErrorMessage = "Password is required")]
  public string Password { get; set; } = "";
}