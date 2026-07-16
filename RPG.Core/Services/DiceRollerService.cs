using RPG.Core.Interfaces.Services;

namespace RPG.Core.Services;

public class DiceRollerService : IDiceRollerService
{
    private readonly Random _dice;

    public DiceRollerService()
    {
        _dice = new Random();
    }


    public int Roll20()
    {
        return _dice.Next(1, 21);
    }

    public int Roll6()
    {
        return _dice.Next(1, 7);
    }

}
