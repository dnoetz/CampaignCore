using RPG.Core.Interfaces;

namespace RPG.Core.Entities.Characters.Necromancer;

public class PlayerNecromancer : Character
{
    public List<ICombatAbility> Abilities { get; } = 
        [
            new AbilityNecrosis(),
            new AbilityReapersMark()
        ];
    public override int MainStat => Intelligence;
    public int UndeadRaised { get; private set; }
    
    
    public PlayerNecromancer(string name) : base(name)
    {
        MaxHitpoints = 100;
        CurrentHitpoints = MaxHitpoints;
        Agility = 2;
        Intelligence = 11;
        Strength = 1;
        Vitality = 3;
        Charisma = 3;
        UndeadRaised = 0;
    }

    public override int DealDamage(string abilityName)
    {
        var ability = Abilities.Find(x => x.Name == abilityName);
        if (ability == null)
        {
            return Abilities[0].Execute(this);
        }
        Console.WriteLine($"Using {ability.Name}");
        return ability.Execute(this);
    }

    public void RaiseUndead()
    {
        if (UndeadRaised >= 4)
        {
            Console.WriteLine("You cannot raise more undead at this time");
            return;
        }
        UndeadRaised++;
    }
}
