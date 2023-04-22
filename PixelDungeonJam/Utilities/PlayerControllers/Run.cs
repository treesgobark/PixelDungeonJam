using PixelDungeonJam.Entities;

namespace PixelDungeonJam.Utilities.PlayerControllers;

public class Run : PlayerController
{
    public Run(Player parent) : base(parent) { }

    protected override string ChainName => "Run";

    public override PlayerController EvaluateExitConditions()
    {
        if (Parent.MovementInput is { X: 0, Y: 0 })
        {
            return DefaultStateAfterTimeout;
        }
        
        return null;
    }
}
