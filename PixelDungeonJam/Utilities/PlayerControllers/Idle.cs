using FlatRedBall.Graphics.Animation;
using PixelDungeonJam.Entities;

namespace PixelDungeonJam.Utilities.PlayerControllers;

public class Idle : PlayerController
{
    public Idle(Player parent) : base(parent) { }

    protected override string ChainName => "Idle";

    public override PlayerController EvaluateExitConditions()
    {
        if (Parent.MovementInput is not { X: 0, Y: 0 })
        {
            return Get<Run>();
        }
        
        return base.EvaluateExitConditions();
    }
}
