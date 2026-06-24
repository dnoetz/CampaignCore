using RPG.Core.Interfaces;

namespace RPG.Core.Entities.Characters.Necromancer;

public class AbilityNecrosis : ICombatAbility
{
    public string Name { get; }
    public string Description { get; }
    public string DamageType { get; }
    public int BaseDamage { get; }

    public AbilityNecrosis()
    {
        Name = "Necrosis";
        Description = "Inflict your foe with dark energy, dealing 30 immediate damage.";
        DamageType = "Plague";
        BaseDamage = 40;
    }

}
