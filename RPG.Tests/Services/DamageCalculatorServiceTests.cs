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
    private readonly Character _necro = new("Necro", 100, 1, 11, 1, 1, 1, PlayableClasses.Necromancer);
    private readonly DamageCalculatorService _sut;
    
    public DamageCalculatorServiceTests()
    {
        var provider = new Mock<IAbilityProvider>();
        provider.Setup(p => p.GetAbilitiesForClass(PlayableClasses.Necromancer))
            .Returns(new List<ICombatAbility> { new AbilityNecrosis() });
        _sut = new DamageCalculatorService(provider.Object);
    }
    
    [Fact]
    public void CalculateCriticalDamage_Necrosis_ReturnsCritical()
    { 
        var damageDone = _sut.CalculateCriticalDamage(_necro, "Necrosis");
        
        Assert.Equal(39, damageDone);
    }
    
    [Fact]
    public void CalculateDamage_Necrosis_ReturnsNonCritical()
    {
        var damageDone = _sut.CalculateDamage(_necro, 4, "Necrosis");
        
        Assert.Equal(30, damageDone);
    }
    
    [Fact]
    public void CalculateCriticalDamage_Foo_ThrowsException()
    {
        var exception =
            Assert.Throws<InvalidOperationException>(() =>
                _sut.CalculateCriticalDamage(_necro, "Foo"));

        Assert.Contains("Ability not found!", exception.Message);
    }
    
    [Fact]
    public void CalculateDamage_Bar_ThrowsException()
    {
        var exception =
            Assert.Throws<InvalidOperationException>(() =>
                _sut.CalculateDamage(_necro, 4, "Bar"));

        Assert.Contains("Ability not found!", exception.Message);
    }
}