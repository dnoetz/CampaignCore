using RPG.Core.Entities.Monsters;

namespace RPG.Core.Interfaces.Providers;

public interface ICacheProvider
{
    Monster GetOrCreateMonsterCache(int campaignId);
    void SetMonsterCache(int campaignId, Monster monster);
}