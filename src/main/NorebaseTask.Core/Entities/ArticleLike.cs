using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NorebaseTask.Core;

public class ArticleLike : BaseEntity
{
    [Required]
    [Column("user_id")]
    public Guid UserId { get; set; }
    
    [Required]
    [Column("article_id")]
    public Guid ArticleId { get; set; }
}