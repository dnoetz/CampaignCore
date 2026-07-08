using RPG.Core.Entities.Campaigns;
using RPG.Core.Enums;
using RPG.Core.Interfaces;

namespace RPG.Core.Entities.Characters;

public class Character
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
    public int MainStat { get; protected set; } 
    public bool IsDead { get; protected set; } = false;
    public PlayableClasses PlayerClass { get; set; }
    
    public User Player { get; set; }
    public int? CampaignId { get; set; }
    public Campaign Campaign { get; set; }

    public Character(
        string name, 
        int maxHitpoints,
        int agility,
        int intelligence,
        int strength,
        int vitality,
        int charisma,
        PlayableClasses playerClass)
    {
        Name = name;
        Level = 1;
        ExperienceToLevel = 10;
        MaxHitpoints = maxHitpoints;
        CurrentHitpoints = maxHitpoints;
        Agility = agility;
        Intelligence = intelligence;
        Strength = strength;
        Vitality = vitality;
        Charisma = charisma;
        PlayerClass = playerClass;
        //revisit main stat functionality after complete character refactor
        MainStat = 10;
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
}
