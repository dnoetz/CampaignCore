using RPG.Core.Entities;
using RPG.Core.Entities.Campaigns;
using RPG.Core.Entities.Characters;
using RPG.Core.Enums;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Repositories;

namespace RPG.Core.Services;

public class CharacterService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CharacterService(ICharacterRepository characterRepository, IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Character> CreateCharacter(
        PlayableClasses playerClass,
        string name,
        Campaign campaign,
        User user)
    {
        
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
            playerClass);

        character.Player = user;
        character.Campaign = campaign;
        await _characterRepository.AddAsync(character);
        return character;
    }
    
    public async Task DeleteCharacterAsync(int characterId)
    {
        await _characterRepository.DeleteAsync(characterId);
        await _unitOfWork.CompleteAsync();
    }
}