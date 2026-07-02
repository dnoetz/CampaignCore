using RPG.Core.Entities.Characters;

namespace RPG.Core.Entities.Campaigns;

public class Campaign
{
    public int Id { get; set; }
    public string Name { get; set; }
    public User Owner { get; set; }
    public List<Character> Characters { get; set; } = new();
    public List<CampaignAction> CampaignActions { get; set; } = new();
}