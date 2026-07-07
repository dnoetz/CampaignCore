using RPG.Core.Entities;
using RPG.Core.Entities.Campaigns;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Repositories;

namespace RPG.Core.Services;

public class CampaignService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICharacterRepository _character;
    private readonly ICampaignRepository _campaign;
    private readonly CampaignCodeService _campaignCode;

    public CampaignService(
        IUnitOfWork unitOfWork,
        ICharacterRepository character,
        ICampaignRepository campaign,
        CampaignCodeService campaignCode)
    {
        _unitOfWork = unitOfWork;
        _character = character;
        _campaign = campaign;
        _campaignCode = campaignCode;
    }

    public async Task<Campaign> CreateCampaign(string name, User owner)
    {
        var tries = 0;
        var codeExists = true;
        var campaignCode = "";
        
        while (tries < 10 && codeExists)
        {
            campaignCode = _campaignCode.GenerateCode();
            codeExists = await _campaign.CodeExistsAsync(campaignCode);
            tries++;
        }

        if (codeExists)
        {
            throw new InvalidOperationException("Could not create campaign, please try again!");
        }

        var campaign = new Campaign()
        {
            Name = name,
            Owner = owner,
            CampaignCode = campaignCode
        };

        await _campaign.AddAsync(campaign);
        await _unitOfWork.CompleteAsync();
        return campaign;
    }
}