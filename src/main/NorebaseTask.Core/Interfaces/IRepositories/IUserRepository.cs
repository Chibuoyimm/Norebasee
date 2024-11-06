using System.Threading.Tasks;

namespace NorebaseTask.Core.Interfaces.IRepositories;

public interface IUserRepository
{
  Task<User?> GetByIdAsync(string id);
  Task<User?> GetByEmailAsync(string email);
  Task<User> AddAsync(User user);
}
