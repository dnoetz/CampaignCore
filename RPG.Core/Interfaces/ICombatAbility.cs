using RPG.Core.Entities.Characters;

namespace RPG.Core.Interfaces;

public interface ICombatAbility
{
    string ReferenceName { get; }
    string Name { get; }
    string Description { get; }
    string DamageType { get; }
    int BaseDamage { get; }
    
    int Execute(Character player);
}
