using RPG.Core.Enums;

namespace RPG.API.DTOs.Character;

public record CharacterResponseDto(
    int Id, 
    string Name, 
    int Level, 
    int ExperienceToLevel,
    int MaxHitpoints,
    int CurrentHitpoints,
    int Agility,
    int Intelligence,
    int Strength,
    int Vitality,
    int Charisma,
    PlayableClasses PlayerClass,
    bool IsDead);