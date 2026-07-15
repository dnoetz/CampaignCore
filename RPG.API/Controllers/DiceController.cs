using Microsoft.AspNetCore.Mvc;
using RPG.Core.Services;

namespace RPG.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiceController : ControllerBase
{
    private readonly DiceRollerService _diceRoller;

    public DiceController(DiceRollerService diceRoller)
    {
        _diceRoller = diceRoller;
    }
    
    [HttpGet("roll-six")]
    public ActionResult<int> RollSixAsync()
    {
        var result = _diceRoller.Roll6();
        return Ok(result);
    }
    
    [HttpGet("roll-twenty")]
    public ActionResult<int> RollTwentyAsync()
    {
        var result = _diceRoller.Roll20();
        return Ok(result);
    }
}