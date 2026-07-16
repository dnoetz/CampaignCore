using Microsoft.Extensions.Caching.Memory;
using RPG.Core.Entities.Monsters;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Providers;

namespace RPG.Infrastructure.Providers;

public class CacheProvider : ICacheProvider
{
    private readonly IMemoryCache _cache;

    public CacheProvider(IMemoryCache cache)
    {
        _cache = cache;
    }
    
    public Monster GetOrCreateMonsterCache(int campaignId)
    {
        var monster = _cache.Get(campaignId);
        
        if (monster is Monster monster1) return monster1;
        
        var newMonster = new Goblin("Goblin");
        monster = _cache.Set(campaignId, newMonster,
            new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(30) });

        return monster as Monster ?? throw new InvalidOperationException("Could not cache monster");
    }

    public void SetMonsterCache(int campaignId, Monster monster)
    {
        _cache.Set(campaignId, monster,
            new MemoryCacheEntryOptions { SlidingExpiration = TimeSpan.FromMinutes(30) });
    }
}