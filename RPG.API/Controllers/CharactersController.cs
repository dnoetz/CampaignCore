using Mapster;
using Microsoft.AspNetCore.Mvc;
using RPG.API.DTOs.Character;
using RPG.Core.Interfaces.Repositories;
using RPG.Core.Services;

namespace RPG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly ICharacterRepository _characterRepository;
    private readonly CharacterService _characterService;
    private readonly ExperienceService _experienceService;

    public CharactersController(ICharacterRepository characterRepository, CharacterService characterService,
        ExperienceService experienceService)
    {
        _characterRepository = characterRepository;
        _characterService = characterService;
        _experienceService = experienceService;
    }

    [HttpGet("{characterId}")]
    public async Task<ActionResult<CharacterResponseDto>> GetFullCharByIdAsync(int characterId)
    {
        var character = await _characterRepository.GetByIdAsync(characterId);

        if (character == null) return NotFound();

        var characterReponse = character.Adapt<CharacterResponseDto>();

        return Ok(characterReponse);
    }

    [HttpGet("all-user-characters")]
    public async Task<ActionResult<IEnumerable<CharacterSummaryDto>>> GetAllCharsByUserIdAsync([FromQuery] int userId)
    {
        var characters = await _characterRepository.GetAllByUserAsync(userId);

        var charactersResponse = characters.Adapt<List<CharacterSummaryDto>>();

        return charactersResponse;
    }

    [HttpPut("levelup/{characterId}")]
    public async Task<ActionResult> CharacterLevelUp(int characterId, CharacterLevelUpDto statUpgrades)
    {
        await _experienceService.IncreaseLevel(characterId, statUpgrades.Hp, statUpgrades.Agility,
            statUpgrades.Intelligence, statUpgrades.Strength, statUpgrades.Vitality, statUpgrades.Charisma);
        return Ok();
    }
    
    [HttpDelete("{characterId}")]
    public async Task<ActionResult> DeleteAsync(int characterId)
    {
        await _characterService.DeleteCharacterAsync(characterId);

        return NoContent();
    }
}