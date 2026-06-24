using RPG.Core.Entities.Monsters;
using RPG.Core.Interfaces;

namespace RPG.Core.Services;

public class CombatService
{
    private readonly Random _dice;
    public CombatService()
    {
        _dice = new Random();
    }

    public void ExecuteTurn(ICombatant player, ICombatant monster)
    {
        int roll = _dice.Next(7);
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
