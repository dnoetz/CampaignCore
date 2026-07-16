using Moq;
using RPG.Core.Entities.Characters;
using RPG.Core.Entities.Characters.Necromancer;
using RPG.Core.Enums;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Providers;
using RPG.Core.Services;

namespace RPG.Tests.Services;

public class DamageCalculatorServiceTests
{
    [Fact]
    public void CalculateCriticalDamage_Necrosis_ReturnsCritical()
    {
        var mockAbilityProvider = new Mock<IAbilityProvider>();

        var character = new Character("Necro", 100, 1, 11, 1, 1, 1, PlayableClasses.Necromancer);

        mockAbilityProvider
            .Setup(service => service.GetAbilitiesForClass(PlayableClasses.Necromancer))
            .Returns( new List<ICombatAbility> { new AbilityNecrosis() });

        var damageCalculatorService = new DamageCalculatorService(mockAbilityProvider.Object);

        var damageDone = damageCalculatorService.CalculateCriticalDamage(character, "Necrosis");
        
        Assert.Equal(39, damageDone);
    }
    
    [Fact]
    public void CalculateDamage_Necrosis_ReturnsNonCritical()
    {
        var mockAbilityProvider = new Mock<IAbilityProvider>();

        var character = new Character("Necro", 100, 1, 11, 1, 1, 1, PlayableClasses.Necromancer);

        mockAbilityProvider
            .Setup(service => service.GetAbilitiesForClass(PlayableClasses.Necromancer))
            .Returns( new List<ICombatAbility> { new AbilityNecrosis() });

        var damageCalculatorService = new DamageCalculatorService(mockAbilityProvider.Object);

        var damageDone = damageCalculatorService.CalculateDamage(character, 4, "Necrosis");
        
        Assert.Equal(30, damageDone);
    }
    
    [Fact]
    public void CalculateCriticalDamage_Foo_ThrowsException()
    {
        var mockAbilityProvider = new Mock<IAbilityProvider>();

        var character = new Character("Necro", 100, 1, 11, 1, 1, 1, PlayableClasses.Necromancer);

        mockAbilityProvider
            .Setup(service => service.GetAbilitiesForClass(PlayableClasses.Necromancer))
            .Returns( new List<ICombatAbility> { new AbilityNecrosis() });

        var damageCalculatorService = new DamageCalculatorService(mockAbilityProvider.Object);

        var exception =
            Assert.Throws<InvalidOperationException>(() =>
                damageCalculatorService.CalculateCriticalDamage(character, "Foo"));

        Assert.Contains("Ability not found!", exception.Message);
    }
    
    [Fact]
    public void CalculateDamage_Bar_ThrowsException()
    {
        var mockAbilityProvider = new Mock<IAbilityProvider>();

        var character = new Character("Necro", 100, 1, 11, 1, 1, 1, PlayableClasses.Necromancer);

        mockAbilityProvider
            .Setup(service => service.GetAbilitiesForClass(PlayableClasses.Necromancer))
            .Returns( new List<ICombatAbility> { new AbilityNecrosis() });

        var damageCalculatorService = new DamageCalculatorService(mockAbilityProvider.Object);

        var exception =
            Assert.Throws<InvalidOperationException>(() =>
                damageCalculatorService.CalculateDamage(character, 4, "Bar"));

        Assert.Contains("Ability not found!", exception.Message);
    }
}