using RPG.Core.Enums;
using RPG.Core.Interfaces;

namespace RPG.Core.Entities.Characters.Necromancer;

public class AbilityNecrosis : ICombatAbility
{
    public string ReferenceName { get; } = "Necrosis";
    public string Name { get; } = "Necrosis";
    public string Description { get; } = $"Inflict your foe with dark energy, dealing immediate damage.";
    public string DamageType { get; } = "Plague";
    public PlayableClasses AllowedClass { get; } = PlayableClasses.Necromancer; 
    public int BaseDamage { get; } = 15;

    public int Execute(Character player)
    {
        return BaseDamage + player.MainStat;
    }
}
