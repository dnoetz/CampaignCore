using RPG.Core.Entities.Characters;
using RPG.Core.Interfaces.Providers;
using RPG.Core.Interfaces.Services;

namespace RPG.Core.Services;

public class DamageCalculatorService : IDamageCalculatorService
{
    private readonly IAbilityProvider _abilityProvider;

    public DamageCalculatorService(IAbilityProvider abilityProvider)
    {
        _abilityProvider = abilityProvider;
    }

    public int CalculateCriticalDamage(Character player, string abilityName)
    {
        var abilities = _abilityProvider.GetAbilitiesForClass(player.PlayerClass);
        var ability = abilities.FirstOrDefault(a => a.ReferenceName == abilityName);
        if (ability == null)
        {
            throw new InvalidOperationException("Ability not found!");
        }
        return Convert.ToInt32(ability.Execute(player) * 1.5);
    }

    public int CalculateDamage(Character player, int rollValue, string abilityName)
    {
        var abilities = _abilityProvider.GetAbilitiesForClass(player.PlayerClass);
        var ability = abilities.FirstOrDefault(a => a.ReferenceName == abilityName);
        if (ability == null)
        {
            throw new InvalidOperationException("Ability not found!");
        }
        return ability.Execute(player) + (rollValue);
    }
}
