using RPG.Core.Entities.Characters;
using RPG.Core.Entities.Monsters;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Repositories;

namespace RPG.Core.Services;

public class CombatService
{
    private readonly DamageCalculatorService _damage;
    private readonly ExperienceService _exp;
    private readonly ICharacterRepository _character;
    private readonly IUnitOfWork _unitOfWork;
    public CombatService(
        DamageCalculatorService damage,
        ExperienceService exp,
        ICharacterRepository character,
        IUnitOfWork unitOfWork)
    {
        _damage = damage;
        _exp = exp;
        _character = character;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteCombatTurn(Character player, Monster enemy, string abilityName, int initiative, int roll)
    {
        if (initiative > 5)
        {
            if (roll == 6)
            {
                enemy.TakeDamage(_damage.CalculateCriticalDamage(player, abilityName));
            }
            else
            {
                enemy.TakeDamage(_damage.CalculateDamage(player, roll, abilityName));
            }
        }
        else
        {
            player.TakeDamage(enemy.DealDamage());
        }
        if (enemy.IsDead)
        {
            _exp.AwardExp(player, enemy);
            if (player.ExperienceToLevel <= 0)
            {
                player.LevelUp(30, 2, 2, 2, 2, 2);
            }
        }

        await _character.UpdateAsync(player);
        await _unitOfWork.CompleteAsync();
    }
}
