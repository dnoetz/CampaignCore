using RPG.Core.Entities.Characters;
using RPG.Core.Entities.Monsters;

namespace RPG.Core.Interfaces.Services;

public interface IExperienceService
{
    void AwardExp(Character player, Monster monster);

    Task IncreaseLevel(int playerId, int hp, int agility, int intelligence, int strength,
        int vitality, int charisma);
}