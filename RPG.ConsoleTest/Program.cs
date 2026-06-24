using RPG.Core.Entities.Characters.Necromancer;
using RPG.Core.Entities.Monsters;
using RPG.Core.Services;

var necromancer = new PlayerNecromancer("Player");
var monster = new Goblin("Goblin");
var combat = new CombatService();

Console.WriteLine($"Player HP: {necromancer.CurrentHitpoints}");
Console.WriteLine($"Monster HP: {monster.CurrentHitpoints}");

while(monster.IsDead == false)
{
combat.ExecuteTurn(necromancer, monster);

Console.WriteLine($"Player HP: {necromancer.CurrentHitpoints}");
Console.WriteLine($"Monster HP: {monster.CurrentHitpoints}");
};