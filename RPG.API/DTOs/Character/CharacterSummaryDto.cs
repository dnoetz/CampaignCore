using RPG.Core.Enums;

namespace RPG.API.DTOs.Character;

public record CharacterSummaryDto(int Id, string Name, PlayableClasses PlayerClass, int Level);