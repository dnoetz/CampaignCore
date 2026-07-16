using RPG.Core.Entities;

namespace RPG.Core.Interfaces.Providers;

public interface ITokenProvider
{
    string GenerateToken(User user);
}