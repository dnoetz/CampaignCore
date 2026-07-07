using RPG.Core.Interfaces;

namespace RPG.Core.Services;

public class CampaignCodeService
{
    private const string alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string symbols = "@#!$";
    
    public string GenerateCode()
    {
        int nums = Random.Shared.Next(100, 1000);
        int symbolSelect = Random.Shared.Next(3);
        char[] letters = new char[3];

        for (int i = 0; i < 3; i++)
        {
            letters[i] = alphabet[Random.Shared.Next(alphabet.Length)];
        }

        string letterString = new string(letters);

        return nums.ToString() + symbols[symbolSelect] + letterString;
    }
}