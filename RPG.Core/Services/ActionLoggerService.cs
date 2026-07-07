using RPG.Core.Entities.Campaigns;
using RPG.Core.Entities.Characters;
using RPG.Core.Enums;
using RPG.Core.Interfaces.Repositories;

namespace RPG.Core.Services;

public class ActionLoggerService
{
    private readonly ICampaignActionRepository _action;

    public ActionLoggerService(ICampaignActionRepository action)
    {
        _action = action;
    }
    
    public async Task LogAction(
        string narrative,
        Character player,
        ActionType actionType,
        string result)
    {
        var action = new CampaignAction()
        {
            ActionType = actionType,
            Actor = player,
            Narrative = narrative,
            Result = result,
            Timestamp = DateTime.UtcNow,
            CampaignId = player.CampaignId ??
                         throw new InvalidOperationException($"{player.Name} is not connected to a campaign!")
        };

        await _action.AddAsync(action);
    }
}