using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using NorebaseTask.Core;
using NorebaseTask.Core.Interfaces.IRepositories;

namespace NorebaseTask.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
  private readonly ApiDbContext _context;

  public UserRepository(ApiDbContext context)
  {
    _context = context;
  }

  public async Task<User?> GetByIdAsync(string id)
  {
    return await _context.Set<User>().FindAsync(Guid.Parse(id));
  }

  public async Task<User?> GetByEmailAsync(string email)
  {
    return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
  }

  public async Task<User> AddAsync(User user)
  {
    _context.Set<User>().Add(user);
    await _context.SaveChangesAsync();
    return user;
  }
}
