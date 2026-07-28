using RPG.API.DTOs.Character;

namespace RPG.API.DTOs.Campaign;

public record PlayableCampaignDto(int Id, 
    string Name, 
    string CampaignCode,
    IReadOnlyList<CharacterSummaryDto> Characters,
    IReadOnlyList<FullCampaignActionDto> CampaignActions);