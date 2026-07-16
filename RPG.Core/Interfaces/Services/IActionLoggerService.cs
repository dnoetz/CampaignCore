using RPG.Core.Entities.Campaigns;
using RPG.Core.Entities.Characters;
using RPG.Core.Enums;

namespace RPG.Core.Interfaces.Services;

public interface IActionLoggerService
{
    Task<CampaignAction> LogAction(string narrative, Character player, ActionType actionType, string result);
}