using RPG.Core.Entities.Campaigns;
using RPG.Core.Entities.Characters;
using RPG.Core.Entities.Monsters;
using RPG.Core.Enums;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Repositories;

namespace RPG.Core.Services;

public class CombatService
{
    private readonly DamageCalculatorService _damage;
    private readonly ExperienceService _exp;
    private readonly ICharacterRepository _character;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICampaignActionRepository _campaignAction;
    public CombatService(
        DamageCalculatorService damage,
        ExperienceService exp,
        ICharacterRepository character,
        IUnitOfWork unitOfWork,
        ICampaignActionRepository campaignAction)
    {
        _damage = damage;
        _exp = exp;
        _character = character;
        _unitOfWork = unitOfWork;
        _campaignAction = campaignAction;
    }

    public async Task ExecuteCombatTurn(
        Character player, 
        Monster enemy, 
        string abilityName, 
        int initiative, 
        int roll, 
        string narrative)
    {
        var action = new CampaignAction()
        {
            Narrative = narrative,
            Actor = player,
            ActionType = ActionType.Combat,
            Timestamp = DateTime.UtcNow,
            CampaignId = player.CampaignId ?? throw new InvalidOperationException($"{player.Name} is not connected to a campaign!")
        };

        string result;
        if (initiative > 5)
        {
            if (roll == 6)
            {
                var crit = _damage.CalculateCriticalDamage(player, abilityName);
                enemy.TakeDamage(crit);
                result = $"{player.Name} deals a critical hit for {crit} damage to {enemy.Name}";
            }
            else
            {
                var damage = _damage.CalculateDamage(player, roll, abilityName);
                enemy.TakeDamage(damage);
                result = $"{player.Name} deals {damage} damage to {enemy.Name}";
            }
        }
        else
        {
            var damage = enemy.DealDamage();
            player.TakeDamage(damage);
            result = $"{player.Name} takes {damage} damage from {enemy.Name}";
        }
        if (enemy.IsDead)
        {
            _exp.AwardExp(player, enemy);
        }

        
        action.Result = result;

        await _campaignAction.AddAsync(action);
        await _character.UpdateAsync(player);
        await _unitOfWork.CompleteAsync();
    }
}
