using Microsoft.EntityFrameworkCore;
using RPG.Core.Entities.Campaigns;
using RPG.Infrastructure.Data;

namespace RPG.Infrastructure.Repositories;

public class CampaignActionRepository
{
    private readonly AppDbContext _context;

    public CampaignActionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CampaignAction?> GetByIdAsync(int id)
    {
        return await _context.CampaignActions
            .FirstOrDefaultAsync(ca => ca.Id == id);
    }

    public async Task<IEnumerable<CampaignAction>> GetAllByCampaignIdAsync(int id)
    {
        return await _context.CampaignActions
            .Where(ca => ca.CampaignId == id)
            .ToListAsync();
    }
    
    public async Task AddAsync(CampaignAction action)
    {
        await _context.CampaignActions.AddAsync(action);
    }

    public Task UpdateAsync(CampaignAction action)
    {
        _context.CampaignActions.Update(action);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var action = await _context.CampaignActions.FindAsync(id);
        if (action != null)
        {
            _context.CampaignActions.Remove(action);
        }
    }
}