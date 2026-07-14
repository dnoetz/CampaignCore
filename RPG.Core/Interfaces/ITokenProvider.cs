using RPG.Core.Entities;

namespace RPG.Core.Interfaces;

public interface ITokenProvider
{
    string GenerateToken(User user);
}