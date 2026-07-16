using RPG.Core.Entities;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Providers;
using RPG.Core.Interfaces.Repositories;
using RPG.Core.Interfaces.Services;

namespace RPG.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateUser(string username, string firstName, string lastName, string password, string email,
        DateTime birthday)
    {
        var hashedPassword = _passwordHasher.HashPassword(password);

        var newUser = new User(username, firstName, lastName, hashedPassword, email, birthday);

        await _userRepository.AddAsync(newUser);

        await _unitOfWork.CompleteAsync();
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        return user;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        return user;
    }

    public async Task UpdateUserAsync(int userId, string username, string email)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null) throw new InvalidOperationException("User doesn't exist");

        user.Username = username;
        user.Email = email;

        await _userRepository.UpdateAsync(user);
        
        await _unitOfWork.CompleteAsync();
    }

    public async Task DeleteUserAsync(int userId)
    {
        await _userRepository.DeleteAsync(userId);
        
        await _unitOfWork.CompleteAsync();
    }
}