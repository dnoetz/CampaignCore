using Moq;
using RPG.Core.Entities.Campaigns;
using RPG.Core.Entities.Characters;
using RPG.Core.Entities.Monsters;
using RPG.Core.Enums;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Repositories;
using RPG.Core.Interfaces.Services;
using RPG.Core.Services;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace RPG.Tests.Services;

public class CombatServiceTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly Character _necro = new("Necro", 100, 1, 11, 1, 1, 1, PlayableClasses.Necromancer);
    private readonly Monster _enemy = new Goblin("Goblin");
    private readonly CombatService _sut;
    private readonly Mock<IUnitOfWork> _mockUoW;
    private readonly Mock<ICharacterRepository> _mockCharacterRepository;
    private readonly Mock<IDamageCalculatorService> _mockDamageCalculatorService;
    private readonly Mock<IExperienceService> _mockExperienceService;
    private readonly Mock<IActionLoggerService> _mockActionLoggerService;

    public CombatServiceTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _mockUoW = new Mock<IUnitOfWork>();
        
        _mockActionLoggerService = new Mock<IActionLoggerService>(MockBehavior.Strict);
        _mockActionLoggerService
            .Setup(l => l.LogAction(It.IsAny<string>(), It.IsAny<Character>(), It.IsAny<ActionType>(),
                It.IsAny<string>()))
            .ReturnsAsync(new CampaignAction()
            {
                ActionType = ActionType.Combat,
                Actor = _necro,
                Narrative = "Necromancer attacks Goblin",
                Result = "Test",
                Timestamp = DateTime.UtcNow,
                CampaignId = 1
            });
        
        _mockExperienceService = new Mock<IExperienceService>();
        
        _mockDamageCalculatorService = new Mock<IDamageCalculatorService>(MockBehavior.Strict);
        _mockDamageCalculatorService
            .Setup(d => d.CalculateDamage(It.IsAny<Character>(), It.IsAny<int>(), It.IsAny<string>()))
            .Returns(30);
        _mockDamageCalculatorService
            .Setup(d => d.CalculateCriticalDamage(It.IsAny<Character>(),  It.IsAny<string>()))
            .Returns(39);
        
        _mockCharacterRepository = new Mock<ICharacterRepository>(MockBehavior.Strict);
        _mockCharacterRepository
            .Setup(r => r.GetByIdAsync(_necro.Id))
            .ReturnsAsync(_necro);

        _mockCharacterRepository
            .Setup(r => r.UpdateAsync(It.IsAny<Character>()))
            .Returns(Task.CompletedTask);
        
        _sut = new CombatService(_mockDamageCalculatorService.Object, _mockExperienceService.Object,
            _mockCharacterRepository.Object, _mockUoW.Object, _mockActionLoggerService.Object);
    }
    
    [Fact]
    public async Task ExecuteCombatTurn_PlayerDealsDamage_WhenInitiativeIsGreaterThan5()
    {
        await _sut.ExecuteCombatTurn(_necro, _enemy, "Necrosis", 15, 4, "Necromancer attacks Goblin");

        Assert.True(_enemy.CurrentHitpoints < _enemy.MaxHitpoints);
    }
    
    [Fact]
    public async Task ExecuteCombatTurn_PlayerReceivesDamage_WhenInitiativeIsLessThan5()
    {
        await _sut.ExecuteCombatTurn(_necro, _enemy, "Necrosis", 3, 4, "Necromancer attacks Goblin");

        Assert.True(_necro.CurrentHitpoints < _necro.MaxHitpoints);
    }
    
    [Fact]
    public async Task ExecuteCombatTurn_PlayerDamageIsCritical_WhenRollIs6()
    {
        await _sut.ExecuteCombatTurn(_necro, _enemy, "Necrosis", 15, 6, "Necromancer attacks Goblin");

        Assert.True(_enemy.CurrentHitpoints < _enemy.MaxHitpoints);
        Assert.Equal((_enemy.MaxHitpoints - 39), _enemy.CurrentHitpoints);
    }

    [Fact]
    public async Task ExecuteCombatTurn_AwardsExp_OnEnemyDeath()
    {
        while (!_enemy.IsDead)
        {
            await _sut.ExecuteCombatTurn(_necro, _enemy, "Necrosis", 15, 6, "Necromancer attacks Goblin");
        }
        
        _mockExperienceService.Verify(e => e.AwardExp(_necro, _enemy), Times.Once());
    }
}