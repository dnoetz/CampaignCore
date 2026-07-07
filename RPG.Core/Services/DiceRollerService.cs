namespace RPG.Core.Services;

public class DiceRollerService
{
    private readonly Random _dice;

    public DiceRollerService()
    {
        _dice = new Random();
    }


    public virtual int Roll20()
    {
        return _dice.Next(1, 21);
    }

    public virtual int Roll6()
    {
        return _dice.Next(1, 7);
    }

}
