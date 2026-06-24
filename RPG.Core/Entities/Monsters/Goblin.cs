namespace RPG.Core.Entities.Monsters;

public class Goblin : Monster
{
    public Goblin(string name) : base(name)
    {
        MaxHitpoints = 75;
        CurrentHitpoints = MaxHitpoints;
        ExperienceAwarded = 2;
    }

}
