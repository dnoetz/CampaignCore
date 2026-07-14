using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RPG.API.DTOs.Campaign;
using RPG.API.DTOs.Combat;
using RPG.Core.Entities.Monsters;
using RPG.Core.Interfaces.Repositories;
using RPG.Core.Services;

namespace RPG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CombatController : ControllerBase
{
    private readonly CombatService _combatService;
    private readonly ICharacterRepository _characterRepository;
    private readonly IMemoryCache _cache;

    public CombatController(CombatService combatService, ICharacterRepository characterRepository, IMemoryCache cache)
    {
        _combatService = combatService;
        _characterRepository = characterRepository;
        _cache = cache;
    }

    [Authorize]
    [HttpPut("combat-turn")]
    public async Task<ActionResult<CampaignActionResponseDto>> ExecuteCombatTurn(CombatRequestDto combatRequest)
    {
        var player = await _characterRepository.GetByIdAsync(combatRequest.CharacterId);

        if (player == null) return NotFound();

        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId) || userId != player.Player.Id)
        {
            return Unauthorized();
        }
        
        var monster = _cache.Get(combatRequest.CampaignId);

        if (monster == null)
        {
            var newMonster = new Goblin("Goblin");
            monster = _cache.Set(combatRequest.CampaignId, newMonster,
                new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(30) });
        }
        
        if (monster is not Monster monster1) return BadRequest();
        
        var combatAction = await _combatService.ExecuteCombatTurn(player, monster1, combatRequest.AbilityName,
            combatRequest.Initiative, combatRequest.Roll, combatRequest.Narrative);

        _cache.Set(combatRequest.CampaignId, monster1,
            new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(30) });

        var combatResponse = new CampaignActionResponseDto(combatAction.Result);

        return Ok(combatResponse);
    }
}