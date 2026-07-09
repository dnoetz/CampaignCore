using RPG.Core.Enums;

namespace RPG.API.DTOs.Character;

public record CreateCharacterRequestDto(PlayableClasses PlayerClass, string Name, string CampaignCode);