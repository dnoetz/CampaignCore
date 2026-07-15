using Moq;
using RPG.Core.Entities.Characters.Necromancer;
using RPG.Core.Entities.Monsters;
using RPG.Core.Services;

using Xunit;

namespace RPG.Tests.Services;

public class CombatServiceTests
{
    /*
    [Fact]
    public void ExecuteCombatTurn_PlayerAbilities_AreSearchable()
    {
        var combat = new CombatService();
        var player = new PlayerNecromancer("Necro");
        var enemy = new Goblin("Goblin");
        
        player.RaiseUndead();
        
        combat.ExecuteCombatTurn(player, enemy, "Necrosis", 15, 4);

        var enemyHealthAfterNecrosis = enemy.CurrentHitpoints;
        var actualDamageNecrosis = enemy.MaxHitpoints - enemyHealthAfterNecrosis;
        var expectedDamageNecrosis = player.DealDamage("Necrosis") + 4;
        
        Assert.True(enemy.CurrentHitpoints < enemy.MaxHitpoints);
        Assert.Equal(expectedDamageNecrosis, actualDamageNecrosis);
        
        combat.ExecuteCombatTurn(player, enemy, "ReapersMark", 15, 4);

        var actualDamageReapersMark = enemyHealthAfterNecrosis - enemy.CurrentHitpoints;
        var expectedDamageReapersMark = player.DealDamage("ReapersMark") + 4;
        
        Assert.True(enemy.CurrentHitpoints < enemyHealthAfterNecrosis);
        Assert.Equal(expectedDamageReapersMark, actualDamageReapersMark);
    }
    */
}