using RPG.Core.Entities.Characters;

namespace RPG.Core.Interfaces.Repositories;

public interface ICharacterRepository
{
    Task<Character?> GetByIdAsync(int id);
    Task<IEnumerable<Character>> GetAllAsync();
    Task<IEnumerable<Character>> GetByCampaignIdAsync();
    Task AddAsync(Character character);
    Task UpdateAsync(Character character);
    Task DeleteAsync(int id);
}