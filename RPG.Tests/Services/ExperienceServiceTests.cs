using System.Net;
using Moq;
using RPG.Core.Entities.Characters;
using RPG.Core.Entities.Characters.Necromancer;
using RPG.Core.Services;
using RPG.Core.Entities.Monsters;
using RPG.Core.Enums;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Repositories;
using RPG.Core.Interfaces.Services;
using Xunit;

namespace RPG.Tests.Services;

public class ExperienceServiceTests
{
    private readonly Character _necro = new("Necro", 100, 1, 11, 1, 1, 1, PlayableClasses.Necromancer);
    private readonly Monster _enemy = new Goblin("Goblin");
    private readonly ExperienceService _sut;
    private readonly Mock<IUnitOfWork> _mockUoW;
    private readonly Mock<ICharacterRepository> _mockCharacterRepository;

    public ExperienceServiceTests()
    {
        _mockUoW = new Mock<IUnitOfWork>();
        _mockCharacterRepository = new Mock<ICharacterRepository>();
        _mockCharacterRepository
            .SetupSequence(r => r.GetByIdAsync(_necro.Id))
            .ReturnsAsync(_necro);
        _sut = new ExperienceService(_mockCharacterRepository.Object, _mockUoW.Object);
    }
    
    [Fact]
    public void AwardExp_Player_ReceivesExp()
    {
        _sut.AwardExp(_necro, _enemy);
        
        Assert.True(_necro.ExperienceToLevel < 10);
    }

    [Fact]
    public async Task LevelUp_Succeeds_IfExpIsLessThan0()
    {
        while (_necro.ExperienceToLevel >= 0)
        {
            _sut.AwardExp(_necro, _enemy);
        }

        await _sut.IncreaseLevel(_necro.Id, 0, 0, 8, 0, 0, 0);
        
        Assert.Equal(2, _necro.Level);
        Assert.Equal(19, _necro.Intelligence);
    }

    [Fact]
    public async Task LevelUp_Fails_IfExpIsGreaterThan0()
    {
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _sut.IncreaseLevel(_necro.Id, 0, 0, 8, 0, 0, 0));

        Assert.Contains("Not enough experience to level!", exception.Message);
    }
}