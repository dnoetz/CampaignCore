using RPG.Core.Entities;

namespace RPG.Core.Interfaces.Services;

public interface IUserService
{
    Task CreateUser(string username, string firstName, string lastName, string password, string email,
        DateTime birthday);

    Task<User?> GetUserByEmailAsync(string email);

    Task<User?> GetUserByIdAsync(int userId);

    Task UpdateUserAsync(int userId, string username, string email);

    Task DeleteUserAsync(int userId);

    Task EnsureEmailOrUsernameAvailable(string username, string email);
}