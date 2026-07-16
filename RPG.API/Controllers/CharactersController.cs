using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPG.API.DTOs.Character;
using RPG.Core.Interfaces.Repositories;
using RPG.Core.Interfaces.Services;
using RPG.Core.Services;

namespace RPG.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly ICharacterRepository _characterRepository;
    private readonly ICharacterService _characterService;
    private readonly IExperienceService _experienceService;

    public CharactersController(ICharacterRepository characterRepository, CharacterService characterService,
        ExperienceService experienceService)
    {
        _characterRepository = characterRepository;
        _characterService = characterService;
        _experienceService = experienceService;
    }

    [HttpGet("get-character/{characterId}")]
    public async Task<ActionResult<CharacterResponseDto>> GetFullCharByIdAsync(int characterId)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        var character = await _characterRepository.GetByIdAsync(characterId);

        if (character == null || userId != character.Player.Id) return NotFound();

        var characterReponse = character.Adapt<CharacterResponseDto>();

        return Ok(characterReponse);
    }

    [HttpGet("get-all-user-characters")]
    public async Task<ActionResult<IEnumerable<CharacterSummaryDto>>> GetAllCharsByUserIdAsync()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        var characters = await _characterRepository.GetAllByUserAsync(userId);

        var charactersResponse = characters.Adapt<List<CharacterSummaryDto>>();

        return charactersResponse;
    }

    [HttpPut("level-up/{characterId}")]
    public async Task<ActionResult> CharacterLevelUp(int characterId, CharacterLevelUpDto statUpgrades)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        var character = await _characterRepository.GetByIdAsync(characterId);
        
        if (character == null || userId != character.Player.Id) return NotFound();
        
        await _experienceService.IncreaseLevel(characterId, statUpgrades.Hp, statUpgrades.Agility,
            statUpgrades.Intelligence, statUpgrades.Strength, statUpgrades.Vitality, statUpgrades.Charisma);
        return Ok();
    }
    
    [HttpDelete("delete-character/{characterId}")]
    public async Task<ActionResult> DeleteAsync(int characterId)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        var character = await _characterRepository.GetByIdAsync(characterId);
        
        if (character == null || userId != character.Player.Id) return NotFound();
        
        await _characterService.DeleteCharacterAsync(characterId);

        return NoContent();
    }
}