using RPG.Core.Enums;
using RPG.Core.Interfaces;

namespace RPG.Core.Entities.Characters.Necromancer;

public class AbilitySummonDead : IUtilityAbility
{
    public string referenceName { get; } = "SummonDead";
    public string Name { get; } = "Raise Undead";
    public string Description { get; } = "Use dark magic to bind the bones of the fallen, raising an undead ally to serve you until the end of combat.";
    public PlayableClasses AllowedClass { get; } = PlayableClasses.Necromancer; 
    
    /*public void Execute(PlayerNecromancer player)
    {
        player.RaiseUndead();
    }*/
}