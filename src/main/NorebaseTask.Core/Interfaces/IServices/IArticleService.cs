
using NorebaseTask.Core.DTOs.Article;
using NorebaseTask.Core.Common.Responses;
using NorebaseTask.Core.DTOs.ArticleLike;

namespace NorebaseTask.Core.Interfaces.IServices;

public interface IArticleService
{
  Task<Response<ArticleResponseDto>> GetOneArticleAsync(Guid articleId);
  Task<Response<List<ArticleResponseDto>>> GetAllArticlesAsync();
  Task<Response<List<ArticleResponseDto>>> GetArticlesByUserAsync(Guid userId);
  Task<Response<ArticleResponseDto>> CreateArticleAsync(ArticleRequestDto articleRequestDto, Guid userId);
  Task<Response<ArticleResponseDto>> UpdateArticleAsync(Guid articleId, ArticleUpdateDto articleUpdateDto, Guid userId);
  Task<Response<string>> DeleteArticleAsync(Guid articleId, Guid userId);
  Task<Response<string>> AddLikeAsync(Guid articleId, Guid userId);
  Task<Response<string>> RemoveLikeAsync(Guid articleId, Guid userId);
  Task<Response<ArticleLikeResponseDto>> GetArticleLikes(Guid articleId, Guid userId);

}