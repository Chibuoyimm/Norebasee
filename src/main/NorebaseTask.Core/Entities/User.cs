using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NorebaseTask.Core;

public class User : BaseEntity
{
    [Required]
    [Column("user_name")]
    public string UserName { get; set; } = "";

    [Required]
    [Column("email")]
    public string Email { get; set; } = "";

    [JsonIgnore]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = "";
    
    [Column("role")]
    public string Role{ get; set; } = "";
}