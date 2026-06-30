using RPG.Core.Interfaces;

namespace RPG.Core.Entities.Characters.Necromancer;

public class AbilityReapersMark : ICombatAbility
{
    public string referenceName { get; } = "ReapersMark";
    public string Name { get; } = "Reaper's Mark";
    public string Description { get; } = "Mark a target for your undead summons, dealing damage for each undead under your command";
    public string DamageType { get; } = "Physical";
    public int BaseDamage { get; } = 7;

    public int Execute(Character player)
    {
        if (player is PlayerNecromancer necromancer)
        {
            return BaseDamage * necromancer.UndeadRaised + necromancer.MainStat;
        }

        return 0;
    }
}