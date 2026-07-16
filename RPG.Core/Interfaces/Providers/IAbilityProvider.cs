using RPG.Core.Enums;

namespace RPG.Core.Interfaces.Providers;

public interface IAbilityProvider
{
    IEnumerable<ICombatAbility> GetAbilitiesForClass(PlayableClasses playableClass);
}