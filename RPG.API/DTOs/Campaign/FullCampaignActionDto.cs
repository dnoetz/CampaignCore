using RPG.Core.Enums;

namespace RPG.API.DTOs.Campaign;

public record FullCampaignActionDto(
    int Id,
    string Narrative,
    ActionType ActionType,
    string Result,
    DateTime Timestamp);