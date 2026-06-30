namespace RPG.Core.Entities.Characters.Rogue;

public class PlayerRogue : Character
{
    public override int MainStat => Agility;
    public PlayerRogue(string name) : base(name)
    {
        MaxHitpoints = 150;
        CurrentHitpoints = MaxHitpoints;
        Agility = 9;
        Intelligence = 1;
        Strength = 2;
        Vitality = 4;
        Charisma = 4;
    }

    public override int DealDamage(string abilityName)
    {
        return 40;
    }
}
