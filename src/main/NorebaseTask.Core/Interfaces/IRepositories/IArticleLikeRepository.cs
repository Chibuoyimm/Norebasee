
namespace NorebaseTask.Core.Interfaces.IRepositories;

public interface IArticleLikeRepository
{
  Task<ArticleLike?> GetLikeAsync(Guid articleId, Guid userId);
  Task<int> GetLikeCountAsync(Guid articleId);
  Task<ArticleLike> AddLikeAsync(ArticleLike like);
  Task RemoveLikeAsync(ArticleLike like);
  Task<bool> IsArticleLikedByUserAsync(Guid articleId, Guid userId);
}