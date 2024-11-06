using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Moq;
using NorebaseTask.Core.DTOs.Article;
using NorebaseTask.Core.Interfaces.IServices;
using NorebaseTask.Core.Common.Responses;
using NorebaseTask.Core;
using NorebaseLikeFeature.API.Controllers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace NorebaseTask.Api.Tests.Controllers
{
  public class ArticlesControllerTests
  {
    private readonly ArticlesController _controller;
    private readonly Mock<IArticleService> _mockArticleService;

    public ArticlesControllerTests()
    {
      _mockArticleService = new Mock<IArticleService>();
      _controller = new ArticlesController(_mockArticleService.Object);
    }

    [Fact]
    public async Task GetArticle_ReturnsOkResult_WhenArticleExists()
    {
      var articleId = Guid.NewGuid();
      var expectedResponse = new Response<ArticleResponseDto>
      {
        StatusCode = StatusCodes.Status200OK,
        Data = new ArticleResponseDto()
      };

      _mockArticleService.Setup(service => service.GetOneArticleAsync(articleId))
          .ReturnsAsync(expectedResponse);

      var result = await _controller.GetArticle(articleId);

      var okResult = Assert.IsType<ObjectResult>(result);
      var response = Assert.IsType<Response<ArticleResponseDto>>(okResult.Value);
      Assert.Equal(expectedResponse.StatusCode, response.StatusCode);
    }

    [Fact]
    public async Task GetAllArticles_ReturnsOkResult()
    {
      var expectedResponse = new Response<List<ArticleResponseDto>>
      {
        StatusCode = StatusCodes.Status200OK,
        Data = new List<ArticleResponseDto>()
      };

      _mockArticleService.Setup(service => service.GetAllArticlesAsync())
          .ReturnsAsync(expectedResponse);

      var result = await _controller.GetAllArticles();

      var okResult = Assert.IsType<ObjectResult>(result);
      var response = Assert.IsType<Response<List<ArticleResponseDto>>>(okResult.Value);
      Assert.Equal(expectedResponse.StatusCode, response.StatusCode);
    }

    [Fact]
    public async Task GetArticlesByUser_ReturnsUnauthorized_WhenUserNotFound()
    {
      var context = new DefaultHttpContext();
      _controller.ControllerContext = new ControllerContext
      {
        HttpContext = context
      };

      var result = await _controller.GetArticlesByUser();

      var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
      Assert.Equal(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
    }

    [Fact]
    public async Task CreateArticle_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
       _controller.ModelState.AddModelError("Title", "Required");

      var result = await _controller.CreateArticle(new ArticleRequestDto());

      var badRequestResult = Assert.IsType<ObjectResult>(result);
      Assert.Equal(StatusCodes.Status400BadRequest, badRequestResult.StatusCode);
    }

    [Fact]
    public async Task CreateArticle_ReturnsOkResult_WhenCreatedSuccessfully()
    {
      var user = new User { Id = Guid.NewGuid() };
      var context = new DefaultHttpContext();
      context.Items["User"] = user;

      _controller.ControllerContext = new ControllerContext
      {
        HttpContext = context
      };

      var articleRequestDto = new ArticleRequestDto
      {
        Title = "Sample Article",
        Body = "This is a test article."
      };

      var response = new Response<ArticleResponseDto>
      {
        StatusCode = StatusCodes.Status200OK,
        Message = "Article created successfully."
      };

      _mockArticleService
        .Setup(service => service.CreateArticleAsync(articleRequestDto, user.Id))
        .ReturnsAsync(response);

    var result = await _controller.CreateArticle(articleRequestDto);

      var okResult = Assert.IsType<ObjectResult>(result);
      Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task UpdateArticle_ReturnsUnauthorized_WhenUserNotFound()
    {
      var articleId = Guid.NewGuid();
      var context = new DefaultHttpContext();
      _controller.ControllerContext = new ControllerContext
      {
        HttpContext = context
      };

      var articleUpdateDto = new ArticleUpdateDto
      {
        Title = "Updated Title",
        Body = "Updated Content"
      };

      var result = await _controller.UpdateArticle(articleId, articleUpdateDto);

      var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
      Assert.Equal(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
    }

    [Fact]
    public async Task DeleteArticle_ReturnsUnauthorized_WhenUserNotFound()
    {
      var context = new DefaultHttpContext();
      _controller.ControllerContext = new ControllerContext
      {
        HttpContext = context
      };

      var result = await _controller.DeleteArticle(Guid.NewGuid());

      Assert.IsType<UnauthorizedResult>(result);
    }
  }
}
