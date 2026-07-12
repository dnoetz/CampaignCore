namespace RPG.API.DTOs.Character;

public record CharacterLevelUpDto(int Hp, int Agility, int Intelligence, int Strength,
    int Vitality, int Charisma);