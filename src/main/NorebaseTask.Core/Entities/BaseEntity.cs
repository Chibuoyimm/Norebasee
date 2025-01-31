using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorebaseTask.Core;

public abstract class BaseEntity
{
    [Key]
    [Required]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}