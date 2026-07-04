using RPG.Core.Entities.Campaigns;

namespace RPG.Core.Interfaces.Repositories;

public interface ICampaignRepository
{
    Task<Campaign> GetByIdAsync(int id);
    Task<IEnumerable<Campaign>> GetAllAsync(int campaignId);
    Task AddAsync(Campaign campaign);
    Task UpdateAsync(Campaign campaign);
    Task DeleteAsync(int id);
}