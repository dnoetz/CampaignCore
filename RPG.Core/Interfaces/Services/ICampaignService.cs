using RPG.Core.Entities;
using RPG.Core.Entities.Campaigns;
using RPG.Core.Enums;

namespace RPG.Core.Interfaces.Services;

public interface ICampaignService
{
    Task<Campaign> CreateCampaign(string name, User owner);

    Task AddCharacterToCampaign(PlayableClasses playerClass, string name, User user, string sharedCampaignCode);

    Task DeleteCampaignAsync(int campaignId);
}