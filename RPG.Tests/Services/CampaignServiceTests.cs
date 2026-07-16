using Moq;
using RPG.Core.Entities;
using RPG.Core.Entities.Campaigns;
using RPG.Core.Interfaces;
using RPG.Core.Interfaces.Repositories;
using RPG.Core.Interfaces.Services;
using RPG.Core.Services;

namespace RPG.Tests.Services;

public class CampaignServiceTests
{
    [Fact]
    public async Task CreateCampaign_Succeeds_FirstTry()
    {
        var mockCampaignRepository = new Mock<ICampaignRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockCharacterService = new Mock<ICharacterService>();
        var mockCampaignCodeService = new Mock<ICampaignCodeService>();

        var user = new User("test", "Larry", "McGee", "password", "larry@test.com", new DateTime(1990, 01, 01));

        mockCampaignRepository
            .Setup(repository => repository.CodeExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(false);
        
        mockCampaignCodeService
            .Setup(c => c.GenerateCode())
            .Returns("123@abc");

        var campaignService = new CampaignService(mockUnitOfWork.Object, mockCharacterService.Object,
            mockCampaignRepository.Object, mockCampaignCodeService.Object);

        var campaign = await campaignService.CreateCampaign("TestCampaign", user);
        
        mockCampaignRepository.Verify(r => r.CodeExistsAsync(It.IsAny<string>()), Times.Once);

        mockCampaignRepository.Verify(r => r.AddAsync(It.IsAny<Campaign>()), Times.Once);

        mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);

        Assert.Equal("123@abc", campaign.CampaignCode);
        Assert.Equal("TestCampaign", campaign.Name);
        Assert.Equal(user, campaign.Owner);
    }
    
    [Fact]
    public async Task CreateCampaign_Succeeds_SecondTry()
    {
        var mockCampaignRepository = new Mock<ICampaignRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockCharacterService = new Mock<ICharacterService>();
        var mockCampaignCodeService = new Mock<ICampaignCodeService>();

        var user = new User("test", "Larry", "McGee", "password", "larry@test.com", new DateTime(1990, 01, 01));

        mockCampaignRepository
            .SetupSequence(repository => repository.CodeExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(true)
            .ReturnsAsync(false);
        
        mockCampaignCodeService
            .Setup(c => c.GenerateCode())
            .Returns("123@abc");

        var campaignService = new CampaignService(mockUnitOfWork.Object, mockCharacterService.Object,
            mockCampaignRepository.Object, mockCampaignCodeService.Object);

        var campaign = await campaignService.CreateCampaign("TestCampaign", user);
        
        mockCampaignRepository.Verify(r => r.CodeExistsAsync(It.IsAny<string>()), Times.Exactly(2));

        mockCampaignRepository.Verify(r => r.AddAsync(It.IsAny<Campaign>()), Times.Once);

        mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);

        Assert.Equal("123@abc", campaign.CampaignCode);
        Assert.Equal("TestCampaign", campaign.Name);
        Assert.Equal(user, campaign.Owner);
    }
    
    [Fact]
    public async Task CreateCampaign_Fails_AfterTenAttempts()
    {
        var mockCampaignRepository = new Mock<ICampaignRepository>();
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        var mockCharacterService = new Mock<ICharacterService>();
        var mockCampaignCodeService = new Mock<ICampaignCodeService>();

        var user = new User("test", "Larry", "McGee", "password", "larry@test.com", new DateTime(1990, 01, 01));

        mockCampaignRepository
            .Setup(repository => repository.CodeExistsAsync(It.IsAny<string>()))
            .ReturnsAsync(true);
        
        mockCampaignCodeService
            .Setup(c => c.GenerateCode())
            .Returns("123@abc");

        var campaignService = new CampaignService(mockUnitOfWork.Object, mockCharacterService.Object,
            mockCampaignRepository.Object, mockCampaignCodeService.Object);

        await Assert.ThrowsAsync<InvalidOperationException>(
                () => campaignService.CreateCampaign("TestCampaign", user));
        
        mockCampaignRepository.Verify(r => r.CodeExistsAsync(It.IsAny<string>()), Times.Exactly(10));
        mockCampaignRepository.Verify(r => r.AddAsync(It.IsAny<Campaign>()), Times.Never);
        mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
    }
}