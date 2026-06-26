using RPG.Core.Entities.Monsters;
using RPG.Core.Interfaces;

namespace RPG.Core.Services;

public class CombatService
{
    private readonly DiceRollerService _roller;
    public CombatService()
    {
        _roller = new DiceRollerService();
    }

    public void ExecuteTurn(ICombatant player, ICombatant monster)
    {
        int roll = _roller.Roll6();
        if (roll > 3)
        {
            monster.TakeDamage(player.DealDamage());
        }
        else
        {
            player.TakeDamage(monster.DealDamage());
        }
    }

}
