namespace RPG.Core.Services;

public class DiceRollerService
{
    private readonly Random _dice;

    public DiceRollerService()
    {
        _dice = new Random();
    }


    public int Roll20()
    {
        return _dice.Next(21);
    }

    public int Roll6()
    {
        return _dice.Next(7);
    }

}
