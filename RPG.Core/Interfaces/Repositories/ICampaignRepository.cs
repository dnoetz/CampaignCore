using RPG.Core.Entities.Campaigns;

namespace RPG.Core.Interfaces.Repositories;

public interface ICampaignRepository
{
    Task<Campaign?> GetByUserIdAsync(int userId, int campaignId);
    Task<IEnumerable<Campaign>> GetAllByUserIdAsync(int userId);
    Task AddAsync(Campaign campaign);
    Task UpdateAsync(Campaign campaign);
    Task DeleteAsync(int id);
    Task<bool> CodeExistsAsync(string code);
    Task<Campaign> GetByCodeAsync(string code);
}