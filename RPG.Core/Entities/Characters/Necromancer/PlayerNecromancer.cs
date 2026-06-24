using RPG.Core.Interfaces;

namespace RPG.Core.Entities.Characters.Necromancer;

public class PlayerNecromancer : Character
{
    public List<ICombatAbility> Abilities { get; protected set; }
    public PlayerNecromancer(string name) : base(name)
    {
        MaxHitpoints = 100;
        CurrentHitpoints = MaxHitpoints;
        Agility = 2;
        Intelligence = 11;
        Strength = 1;
        Vitality = 3;
        Charisma = 3;
        Abilities = new List<ICombatAbility>();
        ICombatAbility Necrosis = new AbilityNecrosis();
        Abilities.Add(Necrosis);
    }

    override public int DealDamage()
    {
        return Abilities[0].BaseDamage;
    }

}
