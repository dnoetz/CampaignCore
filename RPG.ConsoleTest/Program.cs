using RPG.Core.Entities.Characters.Necromancer;
using RPG.Core.Entities.Monsters;
using RPG.Core.Services;

var necromancer = new PlayerNecromancer("Player");
var monster = new Goblin("Goblin");
var combat = new CombatService();

necromancer.RaiseUndead();
necromancer.RaiseUndead();
necromancer.RaiseUndead();
necromancer.RaiseUndead();
Console.WriteLine($"Player HP: {necromancer.CurrentHitpoints}");
Console.WriteLine($"Monster HP: {monster.CurrentHitpoints}");

while(!monster.IsDead)
{
    Console.WriteLine("Turn Begin");
    combat.ExecuteCombatTurn(necromancer, monster, "ReapersMark");

    Console.WriteLine($"Player HP: {necromancer.CurrentHitpoints}");
    Console.WriteLine($"Monster HP: {monster.CurrentHitpoints}");
    Console.WriteLine($"Player Level: {necromancer.Level}");
    Console.WriteLine("Turn End");
}


