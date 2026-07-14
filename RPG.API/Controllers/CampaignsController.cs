using System.Security.Claims;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPG.API.DTOs.Campaign;
using RPG.API.DTOs.Character;
using RPG.Core.Interfaces.Repositories;
using RPG.Core.Services;

namespace RPG.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CampaignsController : ControllerBase
{
    private readonly CampaignService _campaignService;
    private readonly ICampaignRepository _campaignRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICharacterRepository _characterRepository;

    public CampaignsController(CampaignService campaignService, ICampaignRepository campaignRepository,
        IUserRepository userRepository, ICharacterRepository characterRepository)
    {
        _campaignService = campaignService;
        _campaignRepository = campaignRepository;
        _userRepository = userRepository;
        _characterRepository = characterRepository;
    }
    
    [HttpGet("get-campaign/{campaignId}")]
    public async Task<ActionResult<CampaignResponseDto>> GetByUserId(int campaignId)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        var campaign = await _campaignRepository.GetByUserIdAsync(userId, campaignId);

        if (campaign == null)
        {
            return NotFound();
        }

        var campaignDto = campaign.Adapt<CampaignResponseDto>();

        return Ok(campaignDto);
    }
    
    [HttpGet("shared-campaign/{campaignCode}/{characterId}")]
    public async Task<ActionResult<CampaignResponseDto>> GetSharedCampaign(string campaignCode, int characterId)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        var campaign = await _campaignRepository.GetByCodeAsync(campaignCode);
        
        var character = await _characterRepository.GetByIdAsync(characterId);

        if (character == null || campaign == null) return NotFound();
        
        if (character.Campaign.Id != campaign.Id || character.Player.Id != userId)
        {
            return Unauthorized();
        }

        var campaignDto = campaign.Adapt<CampaignResponseDto>();

        return Ok(campaignDto);
    }

    [HttpGet("all-campaigns")]
    public async Task<ActionResult<IEnumerable<CampaignSummaryDto>>> GetAllByUserIdAsync()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        var campaignsByUserId = await _campaignRepository.GetAllByUserIdAsync(userId);

        var campaigns = campaignsByUserId.Adapt<List<CampaignSummaryDto>>();

        return Ok(campaigns);
    }
    
    

    [HttpPost("create-campaign")]
    public async Task<ActionResult<CampaignResponseDto>> CreateCampaignAsync(CreateCampaignRequestDto campaignRequest)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        var owner = await _userRepository.GetByIdAsync(userId);
        
        if (owner == null) return NotFound();
        
        var campaign = await _campaignService.CreateCampaign(campaignRequest.Name, owner);

        var campaignResponse = campaign.Adapt<CampaignResponseDto>();

        return CreatedAtAction(nameof(GetByUserId), new { userId = owner.Id, campaignId = campaign.Id }, campaignResponse);
    }

    [HttpPost("add-character-to-campaign")]
    public async Task<ActionResult> AddCharacterToCampaignAsync(CreateCharacterRequestDto character)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        var user = await _userRepository.GetByIdAsync(userId);
        
        if (user == null) return NotFound();
        
        await _campaignService.AddCharacterToCampaign(character.PlayerClass, character.Name, user,
            character.CampaignCode);
        
        return Created();
    }

    [HttpDelete("delete-campaign/{campaignId}")]
    public async Task<ActionResult> DeleteCampaignAsync(int campaignId)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userIdClaim == null || !int.TryParse(userIdClaim, out var userId))
        {
            return Unauthorized();
        }
        
        var campaign = await _campaignRepository.GetByUserIdAsync(userId, campaignId);

        if (campaign == null) return NotFound();
        
        await _campaignService.DeleteCampaignAsync(campaignId);
        
        return NoContent();
    }
}