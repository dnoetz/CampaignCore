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

    public CharactersController(ICharacterRepository characterRepository)
    {
        _characterRepository = characterRepository;
    }

    [HttpGet("{characterId}")]
    public async Task<ActionResult<CharacterResponseDto>> GetFullCharByIdAsync(int characterId)
    {
        var character = await _characterRepository.GetByIdAsync(characterId);

        if (character == null) return NotFound();
        
        var characterReponse = new CharacterResponseDto(character.Id, character.Name, character.Level, 
            character.ExperienceToLevel, character.MaxHitpoints, character.CurrentHitpoints, character.Agility,
            character.Intelligence, character.Strength, character.Vitality, character.Charisma, character.PlayerClass,
            character.IsDead);

        return Ok(characterReponse);
    }

    [HttpGet("all-user-characters")]
    public async Task<ActionResult<IEnumerable<CharacterSummaryDto>>> GetAllCharsByUserIdAsync([FromQuery] int userId)
    {
        var characters = await _characterRepository.GetAllByUserAsync(userId);
        
        var charactersResponse = new List<CharacterSummaryDto>();

        foreach (var character in characters)
        {
            charactersResponse.Add(new CharacterSummaryDto(character.Id, character.Name, character.PlayerClass, character.Level));
        }

        return charactersResponse;
    }
    
    [HttpDelete("{characterId}")]
    public async Task<ActionResult> DeleteAsync(int characterId)
    {
        await _characterRepository.DeleteAsync(characterId);

        return NoContent();
    }
}