
namespace NorebaseTask.Core.DTOs.Article;

public class ArticleResponseDto
{
  public string Id { get; set; } = "";
  public string UserId { get; set; } = "";
  public string Title { get; set; } = "";
  public string Body { get; set; } = "";
  public int Likes { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}