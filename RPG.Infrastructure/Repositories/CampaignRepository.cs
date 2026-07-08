using Microsoft.EntityFrameworkCore;
using RPG.Core.Entities.Campaigns;
using RPG.Core.Interfaces.Repositories;
using RPG.Infrastructure.Data;

namespace RPG.Infrastructure.Repositories;

public class CampaignRepository : ICampaignRepository
{
    private readonly AppDbContext _context;

    public CampaignRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Campaign?> GetByUserIdAsync(int userId, int campaignId)
    {
        return await _context.Campaigns
            .Where(c => c.Owner.Id == userId)
            .FirstOrDefaultAsync(c => c.Id == campaignId);
    }

    public async Task<IEnumerable<Campaign>> GetAllByUserIdAsync(int userId)
    {
        return await _context.Campaigns
            .Where(c => c.Owner.Id == userId)
            .ToListAsync();
    }
    
    public async Task AddAsync(Campaign campaign)
    {
        await _context.Campaigns.AddAsync(campaign);
    }

    public Task UpdateAsync(Campaign campaign)
    {
        _context.Campaigns.Update(campaign);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var campaign = await _context.Campaigns.FindAsync(id);
        if (campaign != null)
        {
            _context.Campaigns.Remove(campaign);
        }
    }

    public async Task<bool> CodeExistsAsync(string code)
    {
        var codeExists = await _context.Campaigns
            .AnyAsync(cc => cc.CampaignCode == code);

        return codeExists;
    }

    public async Task<Campaign> GetByCodeAsync(string code)
    {
        return await _context.Campaigns
                   .FirstOrDefaultAsync(c => c.CampaignCode == code) ??
               throw new InvalidOperationException("Campaign could not be found");
    }
}