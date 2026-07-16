using RPG.Core.Entities.Characters;

namespace RPG.Core.Interfaces.Services;

public interface IDamageCalculatorService
{
    int CalculateCriticalDamage(Character player, string abilityName);

    int CalculateDamage(Character player, int rollValue, string abilityName);
}