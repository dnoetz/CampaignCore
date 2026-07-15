using RPG.Core.Entities.Monsters;

namespace RPG.Core.Interfaces;

public interface ICacheProvider
{
    Monster GetOrCreateMonsterCache(int campaignId);
    void SetMonsterCache(int campaignId, Monster monster);
}