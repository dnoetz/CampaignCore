using RPG.Core.Entities.Campaigns;

namespace RPG.Core.Interfaces.Repositories;

public interface ICampaignActionRepository
{
    Task<CampaignAction?> GetByIdAsync(int id);
    Task<IEnumerable<CampaignAction>> GetAllByCampaignId(int campaignId);
    Task AddAsync(CampaignAction campaignAction);
    Task UpdateAsync(CampaignAction campaignAction);
    Task DeleteAsync(int id);
}