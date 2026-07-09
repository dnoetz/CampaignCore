using RPG.API.DTOs.Character;
using RPG.API.DTOs.User;

namespace RPG.API.DTOs.Campaign;

public record CampaignResponseDto(int Id, 
    string Name, 
    string CampaignCode,
    IReadOnlyList<CharacterSummaryDto> Characters,
    UserSummaryDto Owner);