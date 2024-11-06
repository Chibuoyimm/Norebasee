using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorebaseTask.Core.Interfaces.IServices;
using NorebaseTask.Core.DTOs.Article;
using NorebaseTask.Core;

namespace NorebaseLikeFeature.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
  private readonly IArticleService _articleService;

  public ArticlesController(IArticleService articleService)
  {
    _articleService = articleService;
  }

  // GET: api/articles/{articleId}
  [HttpGet("{articleId}")]
  public async Task<IActionResult> GetArticle(Guid articleId)
  {
    var response = await _articleService.GetOneArticleAsync(articleId);
    return StatusCode(response.StatusCode, response);
  }

  // GET: api/articles
  [HttpGet]
  public async Task<IActionResult> GetAllArticles()
  {
    var response = await _articleService.GetAllArticlesAsync();
    return StatusCode(response.StatusCode, response);
  }

  // GET: api/articles/user
  [HttpGet("user")]
  public async Task<IActionResult> GetArticlesByUser()
  {
    var _currentUser = (User)HttpContext.Items["User"];
 
    if (_currentUser == null)
      return Unauthorized();

    var response = await _articleService.GetArticlesByUserAsync(_currentUser.Id);
    return StatusCode(response.StatusCode, response);
  }

  // POST: api/articles
  [HttpPost]
  public async Task<IActionResult> CreateArticle([FromBody] ArticleRequestDto articleRequestDto)
  {
    if (!ModelState.IsValid)
      return StatusCode(StatusCodes.Status400BadRequest, ModelState);

    var _currentUser = (User)HttpContext.Items["User"];

    if (_currentUser == null)
      return Unauthorized();

    var response = await _articleService.CreateArticleAsync(articleRequestDto, _currentUser.Id);
    return StatusCode(response.StatusCode, response);
  }

  // PUT: api/articles/{articleId}
  [HttpPut("{articleId}")]
  public async Task<IActionResult> UpdateArticle(Guid articleId, [FromBody] ArticleUpdateDto articleUpdateDto)
  {
    if (!ModelState.IsValid)
      return StatusCode(StatusCodes.Status400BadRequest, ModelState);
    
    var _currentUser = (User)HttpContext.Items["User"];

    if (_currentUser == null)
      return Unauthorized();

    var response = await _articleService.UpdateArticleAsync(articleId, articleUpdateDto, _currentUser.Id);
    return StatusCode(response.StatusCode, response);
  }

  // DELETE: api/articles/{articleId}
  [HttpDelete("{articleId}")]
  public async Task<IActionResult> DeleteArticle(Guid articleId)
  {
    var _currentUser = (User)HttpContext.Items["User"];

    if (_currentUser == null)
      return Unauthorized();
    
    var response = await _articleService.DeleteArticleAsync(articleId, _currentUser.Id);
    return StatusCode(response.StatusCode, response);
  }

  // POST: api/articles/like/{articleId}
  [HttpPost("like/{articleId}")]
  public async Task<IActionResult> LikeArticle(Guid articleId)
  {
    var _currentUser = (User)HttpContext.Items["User"];

    if (_currentUser == null)
      return Unauthorized();

    var response = await _articleService.AddLikeAsync(articleId, _currentUser.Id);
    return StatusCode(response.StatusCode, response);
  }

  // POST: api/articles/unlike/{articleId}
  [HttpPost("unlike/{articleId}")]
  public async Task<IActionResult> UnLikeArticle(Guid articleId)
  {
    var _currentUser = (User)HttpContext.Items["User"];

    if (_currentUser == null)
      return Unauthorized();

    var response = await _articleService.RemoveLikeAsync(articleId, _currentUser.Id);
    return StatusCode(response.StatusCode, response);
  }

  // GET: api/articles/like/{articleId}
  [HttpGet("like/{articleId}")]
  public async Task<IActionResult> GetArticleLikes(Guid articleId)
  {
    var _currentUser = (User)HttpContext.Items["User"];
    
    if (_currentUser == null)
      return Unauthorized();

    var response = await _articleService.GetArticleLikes(articleId, _currentUser.Id);
    return StatusCode(response.StatusCode, response);
  }
}
