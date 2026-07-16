namespace RPG.Core.Interfaces.Services;

public interface ICampaignCodeService
{
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Symbols = "@#!$";

    public string GenerateCode();
}