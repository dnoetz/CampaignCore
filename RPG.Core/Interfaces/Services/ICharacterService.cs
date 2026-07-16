using RPG.Core.Entities;
using RPG.Core.Entities.Campaigns;
using RPG.Core.Entities.Characters;
using RPG.Core.Enums;

namespace RPG.Core.Interfaces.Services;

public interface ICharacterService
{
    Task<Character> CreateCharacter(PlayableClasses playerClass, string name, Campaign campaign, User user);

    Task DeleteCharacterAsync(int characterId);
}