namespace RPG.Core.Entities.Characters.Wizard;

public class PlayerWizard : Character
{
    public override int MainStat => Intelligence;
    public PlayerWizard(string name) : base(name)
    {
        MaxHitpoints = 125;
        CurrentHitpoints = MaxHitpoints;
        Agility = 2;
        Intelligence = 12;
        Strength = 1;
        Vitality = 4;
        Charisma = 1;
    }

    public override int DealDamage(string abilityName)
    {
        return 40;
    }
}
