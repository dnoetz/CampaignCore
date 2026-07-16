using RPG.Core.Entities.Characters;
using RPG.Core.Entities.Monsters;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Repositories;
using RPG.Core.Interfaces.Services;

namespace RPG.Core.Services;

public class ExperienceService : IExperienceService
{
    private readonly ICharacterRepository _characterRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ExperienceService(ICharacterRepository characterRepository, IUnitOfWork unitOfWork)
    {
        _characterRepository = characterRepository;
        _unitOfWork = unitOfWork;
    }
    
    public void AwardExp(Character player, Monster monster)
    {
        player.EarnExp(monster.ExperienceAwarded);
    }

    public async Task IncreaseLevel(int playerId, int hp, int agility, int intelligence, int strength,
        int vitality, int charisma)

    {
        var player = await _characterRepository.GetByIdAsync(playerId) ??
                     throw new InvalidOperationException("Cannot find character!");
        player.LevelUp(hp, agility, intelligence, strength, vitality, charisma);

        await _unitOfWork.CompleteAsync();
    }

}
