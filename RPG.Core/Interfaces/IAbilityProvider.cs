using RPG.Core.Enums;

namespace RPG.Core.Interfaces;

public interface IAbilityProvider
{
    IEnumerable<ICombatAbility> GetAbilitiesForClass(PlayableClasses playableClass);
}