using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPG.API.DTOs.Campaign;
using RPG.API.DTOs.Combat;
using RPG.Core.Interfaces.Providers;
using RPG.Core.Interfaces.Repositories;
using RPG.Core.Interfaces.Services;

namespace RPG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CombatController : ControllerBase
{
    private readonly ICombatService _combatService;
    private readonly ICharacterRepository _characterRepository;
    private readonly ICacheProvider _cache;

    public CombatController(ICombatService combatService, ICharacterRepository characterRepository, ICacheProvider cache)
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

        var monster = _cache.GetOrCreateMonsterCache(combatRequest.CampaignId);
        
        var combatAction = await _combatService.ExecuteCombatTurn(player, monster, combatRequest.AbilityName,
            combatRequest.Initiative, combatRequest.Roll, combatRequest.Narrative);

        _cache.SetMonsterCache(combatRequest.CampaignId, monster);

        var combatResponse = new CampaignActionResponseDto(combatAction.Result);

        return Ok(combatResponse);
    }
}