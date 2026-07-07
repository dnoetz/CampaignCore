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
    private readonly ActionLoggerService _actionLogger;
    public CombatService(
        DamageCalculatorService damage,
        ExperienceService exp,
        ICharacterRepository character,
        IUnitOfWork unitOfWork,
        ActionLoggerService actionLogger)
    {
        _damage = damage;
        _exp = exp;
        _character = character;
        _unitOfWork = unitOfWork;
        _actionLogger = actionLogger;
    }

    public async Task ExecuteCombatTurn(
        Character player, 
        Monster enemy, 
        string abilityName, 
        int initiative, 
        int roll, 
        string narrative)
    {
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

        await _actionLogger.LogAction(narrative, player, ActionType.Combat, result);
        await _character.UpdateAsync(player);
        await _unitOfWork.CompleteAsync();
    }
}
