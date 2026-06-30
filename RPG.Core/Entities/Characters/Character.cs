using RPG.Core.Interfaces;

namespace RPG.Core.Entities.Characters;

public abstract class Character
{
    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public int Level { get; protected set; }
    public int ExperienceToLevel { get; protected set; }
    public int MaxHitpoints { get; protected set; }
    public int CurrentHitpoints { get; protected set; }
    public int Agility { get; protected set; }
    public int Intelligence { get; protected set; }
    public int Strength { get; protected set; }
    public int Vitality { get; protected set; }
    public int Charisma { get; protected set; }
    public abstract int MainStat { get; }
    public bool IsDead { get; protected set; } = false;

    protected Character(string name)
    {
        Name = name;
        Level = 1;
        ExperienceToLevel = 10;
    }

    public void TakeDamage(int damage)
    {
        CurrentHitpoints -= damage / (Vitality * 2);
        if (CurrentHitpoints < 0)
        {
            CurrentHitpoints = 0;
            IsDead = true;
        }
    }

    public void Heal(int amount)
    {
        CurrentHitpoints += amount;
        if (CurrentHitpoints > MaxHitpoints)
        {
            CurrentHitpoints = MaxHitpoints;
        }
    }

    public void EarnExp(int exp)
    {
        ExperienceToLevel -= exp;
    }


    public void LevelUp(int addMaxHP = 0, int addAgi = 0, int addInt = 0, int addStr = 0, int addVit = 0, int addChr = 0)
    {
        Level++;
        ExperienceToLevel = (Level * 10) + ExperienceToLevel;
        MaxHitpoints += addMaxHP;
        CurrentHitpoints = MaxHitpoints;
        Agility += addAgi;
        Intelligence += addInt;
        Strength += addStr;
        Vitality += addVit;
        Charisma += addChr;
    }

    public abstract int DealDamage(string abilityName);

}
