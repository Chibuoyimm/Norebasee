using Microsoft.EntityFrameworkCore;
using NorebaseTask.Core;
using NorebaseTask.Core.Interfaces.IRepositories;

namespace NorebaseTask.Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
  private readonly ApiDbContext _context;

  public ArticleRepository(ApiDbContext context)
  {
    _context = context;
  }

  public async Task<Article?> GetByIdAsync(Guid id)
  {
    var article = await _context.Articles.FindAsync(id);

    return article;
  }

  public async Task<List<Article>> GetAllAsync()
  {
    return await _context.Articles.ToListAsync();
  }

  public async Task<List<Article>> GetArticlesByUserAsync(Guid userId)
  {
    return await _context.Articles
        .Where(a => a.UserId == userId)
        .ToListAsync();
  }


  public async Task<Article> CreateAsync(Article article)
  {
    await _context.Articles.AddAsync(article);
    await _context.SaveChangesAsync();

    return article;
  }

  public async Task<Article> UpdateAsync(Article article)
  {
    // Set the UpdatedAt time to now
    article.UpdatedAt = DateTime.UtcNow;

    _context.Entry(article).CurrentValues.SetValues(article);
    await _context.SaveChangesAsync();

    return article;
  }

  public async Task DeleteAsync(Article article)
  {
    _context.Articles.Remove(article);
    await _context.SaveChangesAsync();
  }
}
