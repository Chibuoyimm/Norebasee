using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorebaseTask.Core;

public class Article : BaseEntity
{
    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("title")]
    public string Title { get; set; } = "";

    [Column("body")]
    public string Body { get; set; } = "";
}