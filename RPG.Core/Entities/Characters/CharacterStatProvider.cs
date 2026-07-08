namespace RPG.Core.Entities.Characters;

public static class CharacterStatProvider
{
    public static Dictionary<string, int> AllocateNecromancerStats()
    {
        return new Dictionary<string, int>()
        {
            {"MaxHitpoints", 100},
            {"Agility", 2},
            {"Intelligence", 11},
            {"Strength", 1},
            {"Vitality", 3},
            {"Charisma", 3}
        };
    }
}