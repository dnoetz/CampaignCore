namespace RPG.API.DTOs.Combat;

public record CombatRequestDto(int CampaignId, int CharacterId, string AbilityName, int Initiative, int Roll, string Narrative);