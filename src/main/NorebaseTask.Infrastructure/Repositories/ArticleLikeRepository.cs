using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NorebaseTask.Core;
using NorebaseTask.Core.Interfaces.IRepositories;

namespace NorebaseTask.Infrastructure.Repositories;

public class ArticleLikeRepository : IArticleLikeRepository
{
  private readonly ApiDbContext _context;

  public ArticleLikeRepository(ApiDbContext context)
  {
    _context = context;
  }

  public async Task<ArticleLike?> GetLikeAsync(Guid articleId, Guid userId)
  {
    return await _context.ArticleLikes
        .FirstOrDefaultAsync(al => al.ArticleId == articleId && al.UserId == userId);
  }

  public async Task<int> GetLikeCountAsync(Guid articleId)
  {
    return await _context.ArticleLikes
        .CountAsync(al => al.ArticleId == articleId);
  }

  public async Task<ArticleLike> AddLikeAsync(ArticleLike like)
  {
    await _context.ArticleLikes.AddAsync(like);
    await _context.SaveChangesAsync();
    return like;
  }

  public async Task RemoveLikeAsync(ArticleLike like)
  {
    _context.ArticleLikes.Remove(like);
    await _context.SaveChangesAsync();
  }

  public async Task<bool> IsArticleLikedByUserAsync(Guid articleId, Guid userId)
  {
    return await _context.ArticleLikes
        .AnyAsync(al => al.ArticleId == articleId && al.UserId == userId);
  }
}
