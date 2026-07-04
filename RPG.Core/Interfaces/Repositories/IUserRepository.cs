using RPG.Core.Entities;

namespace RPG.Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
}