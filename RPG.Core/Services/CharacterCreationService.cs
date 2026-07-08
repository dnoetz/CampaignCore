using RPG.Core.Entities;
using RPG.Core.Entities.Campaigns;
using RPG.Core.Entities.Characters;
using RPG.Core.Enums;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Repositories;

namespace RPG.Core.Services;

public class CharacterCreationService
{
    private readonly ICharacterRepository _characterRepository;

    public CharacterCreationService(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    public async Task<Character> CreateCharacter(
        PlayableClasses playerClass,
        string name,
        Campaign campaign,
        User user,
        string sharedCampaignCode)
    {
        if (sharedCampaignCode != campaign.CampaignCode)
        {
            throw new InvalidOperationException("Invalid campaign code!");
        }
        
        Dictionary<string, int> stats;
        switch (playerClass)
        {
            case PlayableClasses.Necromancer:
                stats = CharacterStatProvider.AllocateNecromancerStats();
                break;
            default:
                throw new InvalidOperationException("Stat allocation failed. Please try again.");
        }
        
        var character = new Character(
            name, 
            stats["MaxHitpoints"],
            stats["Agility"],
            stats["Intelligence"],
            stats["Strength"],
            stats["Vitality"],
            stats["Charisma"],
            playerClass,
            user,
            campaign
            );

        await _characterRepository.AddAsync(character);
        return character;
    }
}