using RPG.Core.Entities.Campaigns;

namespace RPG.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public DateTime Birthday { get; set; }
    public List<Campaign> OwnedCampaigns { get; set; }

    public User(string username, string firstName, string lastName, string passwordHash, string email,
        DateTime birthday)
    {
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
        Email = email;
        Birthday = birthday;
        OwnedCampaigns = [];
    }
}