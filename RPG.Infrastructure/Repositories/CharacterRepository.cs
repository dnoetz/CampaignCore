using Microsoft.EntityFrameworkCore;
using RPG.Core.Entities.Characters;
using RPG.Core.Interfaces.Repositories;
using RPG.Infrastructure.Data;

namespace RPG.Infrastructure.Repositories;

public class CharacterRepository : ICharacterRepository
{
    private readonly AppDbContext _context;

    public CharacterRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Character?> GetByIdAsync(int id)
    {
        return await _context.Characters
            .Include(c => c.Player)
            .Include(c => c.Campaign)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Character>> GetAllByUserAsync(int userId)
    {
        return await _context.Characters
            .Where(c => c.Player.Id == userId)
            .Include(c => c.Campaign)
            .ToListAsync();
    }

    public async Task<IEnumerable<Character>> GetByCampaignIdAsync(int campaignId)
    {
        return await _context.Characters
            .Where(c => c.CampaignId == campaignId)
            .Include(c => c.Player)
            .ToListAsync();
    }

    public async Task AddAsync(Character character)
    {
        await _context.Characters.AddAsync(character);
    }

    public Task UpdateAsync(Character character)
    {
        _context.Characters.Update(character);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id)
    {
        var character = await _context.Characters.FindAsync(id);
        if (character != null)
        {
            _context.Characters.Remove(character);
        }
    }
}