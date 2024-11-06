
using System.ComponentModel.DataAnnotations;

namespace NorebaseTask.Core.DTOs.Article;

public class ArticleRequestDto
{
  [Required(ErrorMessage = "Title is required.")]
  [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
  public string Title { get; set; }

  [Required(ErrorMessage = "Body is required.")]
  [StringLength(5000, ErrorMessage = "Body cannot exceed 5000 characters.")]
  public string Body { get; set; }
}