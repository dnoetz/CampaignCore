using RPG.Core.Entities.Characters;
using RPG.Core.Enums;

namespace RPG.Core.Entities.Campaigns;

public class CampaignAction
{
    public int Id { get; set; }
    public string Narrative { get; set; }
    public Character Actor { get; set; }
    public ActionType ActionType { get; set; }
    public string Result { get; set; }
    public DateTime Timestamp { get; set; }
    
    public int CampaignId { get; set; }
    public Campaign Campaign { get; set; }
}