namespace RPG.Core.Entities;

public class PlayerNecromancer : Character
{
    public PlayerNecromancer(string name) : base(name)
    {
        MaxHitpoints = 100;
        CurrentHitpoints = MaxHitpoints;
        Agility = 2;
        Intelligence = 11;
        Strength = 1;
        Vitality = 3;
        Charisma = 3;
    }

    
}
