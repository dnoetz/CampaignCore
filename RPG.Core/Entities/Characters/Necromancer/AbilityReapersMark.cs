using RPG.Core.Enums;
using RPG.Core.Interfaces;

namespace RPG.Core.Entities.Characters.Necromancer;

public class AbilityReapersMark : ICombatAbility
{
    public string ReferenceName { get; } = "ReapersMark";
    public string Name { get; } = "Reaper's Mark";
    public string Description { get; } = "Mark a target for your undead summons, dealing damage for each undead under your command";
    public string DamageType { get; } = "Physical";
    public PlayableClasses AllowedClass { get; } = PlayableClasses.Necromancer; 
    public int BaseDamage { get; } = 7;

    public int Execute(Character player)
    {
        return (BaseDamage * 2) + player.Intelligence;
    }
}