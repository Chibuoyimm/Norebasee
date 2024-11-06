using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using NorebaseTask.Core.DTOs.Article;
using NorebaseTask.Core.DTOs.ArticleLike;
using NorebaseTask.Core.Common.Responses;
using NorebaseTask.Core.Interfaces.IServices;
using NorebaseTask.Core.Interfaces.IRepositories;

namespace NorebaseTask.Core.Services;

public class ArticleService : IArticleService
{
  private readonly IArticleRepository _articleRepository;
  private readonly IArticleLikeRepository _articleLikeRepository;
  private readonly ILogger<ArticleService> _logger;

  public ArticleService(IArticleRepository articleRepository, IArticleLikeRepository articleLikeRepository, ILogger<ArticleService> logger)
  {
    _articleRepository = articleRepository;
    _articleLikeRepository = articleLikeRepository;
    _logger = logger;
  }

  public async Task<Response<ArticleResponseDto>> GetOneArticleAsync(Guid articleId)
  {
    var response = new Response<ArticleResponseDto>();

    try
    {
      var article = await _articleRepository.GetByIdAsync(articleId);
      if (article == null)
      {
        response.StatusCode = StatusCodes.Status404NotFound;
        response.Message = $"Article not found.";
        return response;
      }

      var articleDto = new ArticleResponseDto
      {
        Id = article.Id.ToString(),
        UserId = article.UserId.ToString(),
        Title = article.Title,
        Body = article.Body,
        CreatedAt = article.CreatedAt,
        UpdatedAt = article.UpdatedAt
      };

      response.StatusCode = StatusCodes.Status200OK;
      response.Message = "Article retrieved successfully.";
      response.Data = articleDto;
    }
    catch (Exception ex)
    {
      // Handle specific exceptions
      _logger.LogError(ex, "An error occurred while fetching the article with ID {ArticleId}", articleId);
      response.StatusCode = StatusCodes.Status500InternalServerError;
      response.Message = $"An error occurred: {ex.Message}";
    }

    return response;
  }

  public async Task<Response<List<ArticleResponseDto>>> GetAllArticlesAsync()
  {
    var response = new Response<List<ArticleResponseDto>>();

    try
    {
      var articles = await _articleRepository.GetAllAsync();
      if (articles == null || !articles.Any())
      {
        response.StatusCode = StatusCodes.Status404NotFound;
        response.Message = "No articles found.";
        response.Data = null;
        return response;
      }

      var articleResponseDtos = articles.Select(article => new ArticleResponseDto
      {
        Id = article.Id.ToString(),
        UserId = article.UserId.ToString(),
        Title = article.Title,
        Body = article.Body,
        CreatedAt = article.CreatedAt,
        UpdatedAt = article.UpdatedAt
      }).ToList();

      response.StatusCode = StatusCodes.Status200OK;
      response.Message = "Articles retrieved successfully.";
      response.Data = articleResponseDtos;
    }
    catch (Exception ex)
    {
      // Handle specific exceptions
      _logger.LogError(ex, "An error occurred while fetching all articles.");
      response.StatusCode = StatusCodes.Status500InternalServerError;
      response.Message = $"An error occurred: {ex.Message}";
    }

    return response;
  }

  public async Task<Response<List<ArticleResponseDto>>> GetArticlesByUserAsync(Guid userId)
  {
    var response = new Response<List<ArticleResponseDto>>();

    try
    {
      var articles = await _articleRepository.GetArticlesByUserAsync(userId);

      // Check if articles exist for the user
      if (articles == null || !articles.Any())
      {
        response.StatusCode = StatusCodes.Status404NotFound;
        response.Message = $"No articles found for user.";
        response.Data = null;
        return response;
      }

      var articleResponseDtos = articles.Select(article => new ArticleResponseDto
      {
        Id = article.Id.ToString(),
        UserId = article.UserId.ToString(),
        Title = article.Title,
        Body = article.Body,
        CreatedAt = article.CreatedAt,
        UpdatedAt = article.UpdatedAt
      }).ToList();

      response.StatusCode = StatusCodes.Status200OK;
      response.Message = "Articles retrieved successfully.";
      response.Data = articleResponseDtos;
      return response;
    }
    catch (Exception ex)
    {
      // Handle specific exceptions
      _logger.LogError(ex, "An error occurred while retrieving articles for user with ID {UserId}", userId);
      throw new Exception($"An error occurred while retrieving articles: {ex.Message}");
    }
  }

  public async Task<Response<ArticleResponseDto>> CreateArticleAsync(ArticleRequestDto articleRequestDto, Guid userId)
  {
    var response = new Response<ArticleResponseDto>();

    try
    {
      var article = new Article
      {
        Id = Guid.NewGuid(),
        UserId = userId,
        Title = articleRequestDto.Title,
        Body = articleRequestDto.Body,
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
      };

      var savedArticle = await _articleRepository.CreateAsync(article);

      var articleResponseDto = new ArticleResponseDto
      {
        Id = savedArticle.Id.ToString(),
        UserId = savedArticle.UserId.ToString(),
        Title = savedArticle.Title,
        Body = savedArticle.Body,
        CreatedAt = savedArticle.CreatedAt,
        UpdatedAt = savedArticle.UpdatedAt
      };

      response.StatusCode = StatusCodes.Status201Created;
      response.Message = "Article created successfully.";
      response.Data = articleResponseDto;
    }
    catch (Exception ex)
    {
      // Handle specific exceptions
      _logger.LogError(ex, "An error occurred while creating an article for user with ID {UserId}", userId);
      response.StatusCode = StatusCodes.Status500InternalServerError;
      response.Message = $"An error occurred: {ex.Message}";
    }

    return response;
  }

  public async Task<Response<ArticleResponseDto>> UpdateArticleAsync(Guid articleId, ArticleUpdateDto articleUpdateDto, Guid userId)
  {
    var response = new Response<ArticleResponseDto>();

    try
    {
      // Find the existing article
      var existingArticle = await _articleRepository.GetByIdAsync(articleId);
      if (existingArticle == null)
      {
        response.StatusCode = StatusCodes.Status404NotFound;
        response.Message = $"Article not found.";
        return response;
      }

      // Check if the user is authorized to update the article
      if (existingArticle.UserId != userId)
      {
        response.StatusCode = StatusCodes.Status403Forbidden;
        response.Message = "You are not authorized to update this article.";
        return response;
      }

      // Update the properties if new values are provided
      existingArticle.Title = !string.IsNullOrEmpty(articleUpdateDto.Title) ? articleUpdateDto.Title : existingArticle.Title;
      existingArticle.Body = !string.IsNullOrEmpty(articleUpdateDto.Body) ? articleUpdateDto.Body : existingArticle.Body;

      var updatedArticle = await _articleRepository.UpdateAsync(existingArticle);

      var articleResponseDto = new ArticleResponseDto
      {
        Id = updatedArticle.Id.ToString(),
        UserId = updatedArticle.UserId.ToString(),
        Title = updatedArticle.Title,
        Body = updatedArticle.Body,
        CreatedAt = updatedArticle.CreatedAt,
        UpdatedAt = updatedArticle.UpdatedAt
      };

      response.StatusCode = StatusCodes.Status200OK;
      response.Message = "Article updated successfully.";
      response.Data = articleResponseDto;
    }
    catch (Exception ex)
    {
      // Handle specific exceptions
      _logger.LogError(ex, "An error occurred while updating article with ID {ArticleId}", articleId);
      response.StatusCode = StatusCodes.Status500InternalServerError;
      response.Message = $"An error occurred: {ex.Message}";
    }

    return response;
  }

  public async Task<Response<string>> DeleteArticleAsync(Guid articleId, Guid userId)
  {
    var response = new Response<string>();

    try
    {
      // Find the article
      var article = await _articleRepository.GetByIdAsync(articleId);

      if (article == null)
      {
        response.StatusCode = StatusCodes.Status404NotFound;
        response.Message = $"Article not found.";
        return response;
      }

      // Check if the user is authorized to delete the article
      if (article.UserId != userId)
      {
        response.StatusCode = StatusCodes.Status403Forbidden;
        response.Message = "You are not authorized to delete this article.";
        return response;
      }

      // Perform deletion
      await _articleRepository.DeleteAsync(article);

      response.StatusCode = StatusCodes.Status200OK;
      response.Message = "Article deleted successfully.";
      response.Data = articleId.ToString();
      return response;
    }
    catch (Exception ex)
    {
      // Handle specific exceptions
      _logger.LogError(ex, "An error occurred while deleting the article with ID {ArticleId}", articleId);
      throw new Exception($"An error occurred while deleting the article: {ex.Message}");
    }
  }

  public async Task<Response<string>> AddLikeAsync(Guid articleId, Guid userId)
  {
    var response = new Response<string>();

    try
    {
      var article = await _articleRepository.GetByIdAsync(articleId);
      if (article == null)
      {
        response.StatusCode = StatusCodes.Status404NotFound;
        response.Message = "Article not found.";
        return response;
      }

      // Check if the article has already been liked by the user
      var isLiked = await _articleLikeRepository.IsArticleLikedByUserAsync(articleId, userId);
      if (isLiked)
      {
        response.StatusCode = StatusCodes.Status400BadRequest;
        response.Message = "You have already liked this article.";
        return response;
      }

      var like = new ArticleLike
      {
        ArticleId = articleId,
        UserId = userId
      };
      await _articleLikeRepository.AddLikeAsync(like);

      response.StatusCode = StatusCodes.Status200OK;
      response.Message = "Like added successfully.";
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occurred while adding like for article ID {ArticleId} by user ID {UserId}", articleId, userId);
      response.StatusCode = StatusCodes.Status500InternalServerError;
      response.Message = $"An error occurred: {ex.Message}";
    }

    return response;
  }

  public async Task<Response<string>> RemoveLikeAsync(Guid articleId, Guid userId)
  {
    var response = new Response<string>();

    try
    {
      var article = await _articleRepository.GetByIdAsync(articleId);
      if (article == null)
      {
        response.StatusCode = StatusCodes.Status404NotFound;
        response.Message = "Article not found.";
        return response;
      }

      // Check if the article is liked by the user
      var isLiked = await _articleLikeRepository.IsArticleLikedByUserAsync(articleId, userId);
      if (!isLiked)
      {
        response.StatusCode = StatusCodes.Status400BadRequest;
        response.Message = "You have not liked this article.";
        return response;
      }

      var like = new ArticleLike
      {
        ArticleId = articleId,
        UserId = userId
      };
      await _articleLikeRepository.RemoveLikeAsync(like);

      response.StatusCode = StatusCodes.Status200OK;
      response.Message = "Like removed successfully.";
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An error occurred while removing like for article ID {ArticleId} by user ID {UserId}", articleId, userId);
      response.StatusCode = StatusCodes.Status500InternalServerError;
      response.Message = $"An error occurred: {ex.Message}";
    }

    return response;
  }

  public async Task<Response<ArticleLikeResponseDto>> GetArticleLikes(Guid articleId, Guid userId)
  {
    var response = new Response<ArticleLikeResponseDto>();

    try
    {
      var article = await _articleRepository.GetByIdAsync(articleId);
      if (article == null)
      {
        response.StatusCode = StatusCodes.Status404NotFound;
        response.Message = "Article not found.";
        return response;
      }

      var totalLikes = await _articleLikeRepository.GetLikeCountAsync(articleId);
      var isLiked = await _articleLikeRepository.IsArticleLikedByUserAsync(articleId, userId);

      var articleLikeDto = new ArticleLikeResponseDto
      {
        TotalLikes = totalLikes,
        LikedByUser = isLiked
      };

      response.StatusCode = StatusCodes.Status200OK;
      response.Message = "Article likes retrieved successfully.";
      response.Data = articleLikeDto;
    }
    catch (Exception ex)
    {
      // Handle specific exceptions
      _logger.LogError(ex, "An error occurred while retrieving likes for article ID {ArticleId} by user ID {UserId}", articleId, userId);
      response.StatusCode = StatusCodes.Status500InternalServerError;
      response.Message = $"An error occurred: {ex.Message}";
    }

    return response;
  }

}
