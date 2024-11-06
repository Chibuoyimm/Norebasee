
using System.ComponentModel.DataAnnotations;

namespace NorebaseTask.Core.DTOs.Article;

public class ArticleUpdateDto
{
  [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
  public string Title { get; set; }

  [StringLength(5000, ErrorMessage = "Body cannot exceed 5000 characters.")]
  public string Body { get; set; }
}