
namespace NorebaseTask.Core.Interfaces.IRepositories;

public interface IArticleRepository
{
  Task<Article?> GetByIdAsync(Guid id);
  Task<List<Article>> GetAllAsync();
  Task<List<Article>> GetArticlesByUserAsync(Guid userId);
  Task<Article> CreateAsync(Article article);
  Task<Article> UpdateAsync(Article article);
  Task DeleteAsync(Article article);
}
