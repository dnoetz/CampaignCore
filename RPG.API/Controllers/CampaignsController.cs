using Mapster;
using Microsoft.AspNetCore.Mvc;
using RPG.API.DTOs.Campaign;
using RPG.API.DTOs.Character;
using RPG.Core.Interfaces.Repositories;
using RPG.Core.Services;

namespace RPG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CampaignsController : ControllerBase
{
    private readonly CampaignService _campaignService;
    private readonly ICampaignRepository _campaignRepository;
    private readonly IUserRepository _userRepository;

    public CampaignsController(CampaignService campaignService, ICampaignRepository campaignRepository, IUserRepository userRepository)
    {
        _campaignService = campaignService;
        _campaignRepository = campaignRepository;
        _userRepository = userRepository;
    }
    
    [HttpGet("{userId}/{campaignId}")]
    public async Task<ActionResult<CampaignResponseDto>> GetByUserId(int userId, int campaignId)
    {
        var campaign = await _campaignRepository.GetByUserIdAsync(userId, campaignId);

        if (campaign == null)
        {
            return NotFound();
        }

        var campaignDto = campaign.Adapt<CampaignResponseDto>();

        return Ok(campaignDto);
    }

    [HttpGet("{userId}/all")]
    public async Task<ActionResult<IEnumerable<CampaignSummaryDto>>> GetAllByUserIdAsync(int userId)
    {
        var campaignsByUserId = await _campaignRepository.GetAllByUserIdAsync(userId);

        var campaigns = campaignsByUserId.Adapt<List<CampaignSummaryDto>>();

        return Ok(campaigns);
    }

    [HttpPost("create-campaign")]
    public async Task<ActionResult<CampaignResponseDto>> CreateCampaign(CreateCampaignRequestDto campaignRequest, [FromQuery] int userId)
    {
        var owner = await _userRepository.GetByIdAsync(userId);
        
        if (owner == null) return NotFound();
        
        var campaign = await _campaignService.CreateCampaign(campaignRequest.Name, owner);

        var campaignResponse = campaign.Adapt<CampaignResponseDto>();

        return CreatedAtAction(nameof(GetByUserId), new { userId = owner.Id, campaignId = campaign.Id }, campaignResponse);
    }

    [HttpPost("add-character-to-campaign")]
    public async Task<ActionResult> AddCharacterToCampaignAsync(CreateCharacterRequestDto character, [FromQuery] int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return NotFound();
        
        await _campaignService.AddCharacterToCampaign(character.PlayerClass, character.Name, user,
            character.CampaignCode);
        
        return Created();
    }

    [HttpDelete("{campaignId}")]
    public async Task<ActionResult> DeleteCampaignAsync(int campaignId)
    {
        await _campaignService.DeleteCampaignAsync(campaignId);
        
        return NoContent();
    }
}