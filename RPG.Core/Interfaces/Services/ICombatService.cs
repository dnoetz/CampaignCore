using RPG.Core.Entities.Campaigns;
using RPG.Core.Entities.Characters;
using RPG.Core.Entities.Monsters;

namespace RPG.Core.Interfaces.Services;

public interface ICombatService
{
    Task<CampaignAction> ExecuteCombatTurn(Character player, Monster enemy, string abilityName, int initiative,
        int roll, string narrative);
}