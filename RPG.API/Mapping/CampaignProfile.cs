using Mapster;
using RPG.API.DTOs.Campaign;
using RPG.Core.Entities.Campaigns;

namespace RPG.API.Mapping;

public class CampaignProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Campaign, CampaignSummaryDto>();
        config.NewConfig<Campaign, CampaignResponseDto>();
    }
}