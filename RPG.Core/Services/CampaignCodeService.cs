using RPG.Core.Interfaces.Services;

namespace RPG.Core.Services;

public class CampaignCodeService : ICampaignCodeService
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Symbols = "@#!$";
    
    public string GenerateCode()
    {
        int nums = Random.Shared.Next(100, 1000);
        int symbolSelect = Random.Shared.Next(3);
        char[] letters = new char[3];

        for (int i = 0; i < 3; i++)
        {
            letters[i] = Alphabet[Random.Shared.Next(Alphabet.Length)];
        }

        string letterString = new string(letters);

        return nums.ToString() + Symbols[symbolSelect] + letterString;
    }
}